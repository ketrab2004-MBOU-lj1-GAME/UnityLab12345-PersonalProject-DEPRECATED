using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 35f;
    public float maxAge = 5f;
    private float startAge;

    private void Start()
    {
        startAge = Time.fixedTime;
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (Time.fixedTime - startAge > maxAge)
        {
            //TODO Destroy bullet
        }
    }
}
