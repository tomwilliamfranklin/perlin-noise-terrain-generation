using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isVisible : MonoBehaviour
{
    Renderer m_Renderer;
    // Use this for initialization
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Renderer.isVisible)
        {

            GameObject.Destroy(this);
            Debug.Log("Object is visible " + this.name.ToString());
        }
        else Debug.Log("Object is visible " + this.name.ToString());
    }
}
