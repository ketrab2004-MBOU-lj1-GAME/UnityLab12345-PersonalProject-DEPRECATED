using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 35f;
    public float maxAge = 5f;
    public Vector3 inheritedVel = Vector3.zero;
    private float startAge;

    private void Start()
    {
        startAge = Time.fixedTime;
    }

    void Update()
    {
        transform.Translate((transform.up * speed + (inheritedVel)) * Time.deltaTime, Space.World);
        //move forward and add inheritedVel, only mult with deltatime after so faster*?, in space.world because inheritedVel is

        if (Time.fixedTime - startAge > maxAge)
        {
            Destroy(gameObject);
        }
    }
}
