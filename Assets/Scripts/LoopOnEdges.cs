using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopOnEdges : MonoBehaviour
{
    private float gameWidth = 5f*1.78f;
    private float gameHeight = 5f;
    
    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.x > gameWidth)
        {
            transform.position = new Vector3(-gameWidth, pos.y,pos.z);
        }else if (pos.x < -gameWidth)
        {
            transform.position = new Vector3(gameWidth, pos.y,pos.z);
        }
        if (pos.z > gameHeight)
        {
            transform.position = new Vector3(pos.x, pos.y,-gameHeight);
        }else if (pos.z < -gameHeight)
        {
            transform.position = new Vector3(pos.x, pos.y,gameHeight);
        }
    }
}
