

using Model;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class randomgen : MonoBehaviour
{
    public GameObject currentBlockType;
    public int chunkSize = 10;
    public int worldSize = 10;
    public float freq = 10f;
    public int amp = 10;
    public int worldHeight = 10;
    public int h = 0;

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
                float y = Mathf.Round(Mathf.PerlinNoise(x/freq, z/freq) * amp);

                for(int i = worldHeight + (int) y; i > 0; i--)
                {
                    var newBlock = new GameObject();
                    switch(i)
                    {
                        case var expression when i == worldHeight + (int) y:
                            newBlock = GameObject.Instantiate(_Dictionary["grass"]);
                            break;
                        default:
                            newBlock = GameObject.Instantiate(_Dictionary["dirt"]);
                            break;
                    }
                    newBlock.transform.position = new Vector3(x, i, z);
                }



            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
