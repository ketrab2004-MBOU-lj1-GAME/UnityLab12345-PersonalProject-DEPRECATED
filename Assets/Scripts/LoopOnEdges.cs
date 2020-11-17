using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopOnEdges : MonoBehaviour
{
    readonly float gameWidth = 5f*1.78f;
    readonly float gameHeight = 5f;
    
    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.x > gameWidth) //x too far right
        {
            transform.position = new Vector3(-gameWidth, pos.y,pos.z);
        }else if (pos.x < -gameWidth) //x too far left
        {
            transform.position = new Vector3(gameWidth, pos.y,pos.z);
        }
        if (pos.z > gameHeight) //z too far up (with topdown view)
        {
            transform.position = new Vector3(pos.x, pos.y,-gameHeight);
        }else if (pos.z < -gameHeight) //z too far down (with topdown view)
        {
            transform.position = new Vector3(pos.x, pos.y,gameHeight);
        }
    }
}
