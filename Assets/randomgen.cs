

using Model;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Model;
using System;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class randomgen : MonoBehaviour
{
    public int chunkSize = 10;
    public float pattern = 10f;
    public int noiseIntensity;
    public int worldHeight = 10;

    public Material tempMaterial;

    private Dictionary<string, GameObject> _Dictionary;
    [SerializeField] List<string> _Keys;
    [SerializeField] List<GameObject> _Values;

    // Start is calleaewdwaqdawdd before the first frame update
    void Start()
    {

        int cols = chunkSize;
        int rows = chunkSize;

        _Dictionary = _Keys.Zip(_Values, (k, v) => new { k, v })
              .ToDictionary(x => x.k, x => x.v);

        for (int x = 0; x < cols; x++)
        {
            for(int z= 0; z < rows; z++)
            {
                float y = Mathf.Round(Mathf.PerlinNoise(x/ pattern, z/ pattern) * noiseIntensity);

                for(int i = worldHeight + (int) y; i > 0; i--)
                {
                    switch(i)
                    {
                        case var expression when i == worldHeight + (int)y:
                            var newBlock = GameObject.Instantiate(_Dictionary["grass"]);
                            newBlock.transform.parent = transform;
                            newBlock.transform.position = new Vector3(x, i, z);
                            break;
                        default:
                            newBlock = GameObject.Instantiate(_Dictionary["dirt"]);
                            newBlock.transform.parent = transform;
                            newBlock.transform.position = new Vector3(x, i, z);
                            break;
                    }
                }
            }
        }

   CombineMeshes();


    }

    public void CombineMeshes()
    {
        List<List<CombinedGameObject>> combineLists = new List<List<CombinedGameObject>>();

        Dictionary<string, List<MeshFilter>> categorisedMeshFilters = new Dictionary<string, List<MeshFilter>>();

        foreach (Transform child in transform)
        {
            if (!categorisedMeshFilters.ContainsKey(child.tag))
                categorisedMeshFilters.Add(child.tag, new List<MeshFilter>());

            categorisedMeshFilters[child.tag].Add(child.GetComponent<MeshFilter>());
        }

        foreach (List<MeshFilter> meshFilters in categorisedMeshFilters.Values)
        {
            try
            {
                combineLists.Add(new List<CombinedGameObject>());

                CombinedGameObject[] combine = new CombinedGameObject[meshFilters.Count];

                int i = 0;
                int vertexCount = 0;
                while (i < meshFilters.Count)
                {
                    vertexCount += meshFilters[i].mesh.vertexCount;
                    if (vertexCount > 65536)
                    {
                        vertexCount = 0;
                        combineLists.Add(new List<CombinedGameObject>());
                    }
                    else
                    {
                        combine[i] = new CombinedGameObject();
                        combine[i].combinedInstance = new CombineInstance();
                        combine[i].combinedInstance.mesh = meshFilters[i].mesh;
                        combine[i].combinedInstance.transform = meshFilters[i].transform.localToWorldMatrix;
                        combine[i].gameObject = meshFilters[i];
                        combineLists.Last().Add(combine[i]);
                    }
                    meshFilters[i].gameObject.SetActive(false);
                    i++;
                }

            } catch (Exception e )
            {
                Debug.Log("Error " + e);
            }
        }


  


            Transform chunk = new GameObject("chunk-parent").transform;
            chunk.transform.parent = transform;
            foreach (List<CombinedGameObject> list in combineLists)
            {
                GameObject block = new GameObject("chunk");
                block.transform.parent = chunk;
                MeshFilter mf = block.AddComponent<MeshFilter>();
                MeshRenderer mr = block.AddComponent<MeshRenderer>();
                mr.material = tempMaterial;
                mf.mesh.CombineMeshes(list.Select(x => x.combinedInstance).ToArray());

                list.ForEach(obj =>
                {
                    obj.gameObject.transform.parent = block.transform;
                });
            }
    }
}
