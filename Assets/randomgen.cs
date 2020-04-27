using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomgen : MonoBehaviour
{
    public GameObject currentBlockType;

    public float amp = 10f;
    public float freq = 10f;
    public int worldHeight = 5;
    // Start is calleaewdwaqdawdd before the first frame update
    void Start()
    {
        int cols = 100;
        int rows = 100;

        for (int x = 0; x < cols; x++)
        {
            for(int z= 0; z < rows; z++)
            {
                float y = Mathf.Round(Mathf.PerlinNoise(x/freq, z/freq) * amp);

                for(int i = worldHeight + (int) y; i > 0; i--)
                {   
                    
                    var newBlock = GameObject.Instantiate(currentBlockType);
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
