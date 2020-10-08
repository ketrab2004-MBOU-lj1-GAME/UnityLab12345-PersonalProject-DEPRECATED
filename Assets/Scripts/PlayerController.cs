using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rocketSpeed = 50f;
    public float turnSpeed = 180f;
    
    public float shootCooldown = .1f;
    private float lastShot = -1f;
    
    public GameObject bulletPrefab;
    public Rigidbody rigidBody;

    // Update is called once per frame
    void Update()
    {
        //Rotate
        transform.Rotate(Vector3.forward, turnSpeed * Mathf.Pow(-Input.GetAxis("Horizontal"),3) * Time.deltaTime);
        //Propulsion
        rigidBody.AddForce(transform.up * rocketSpeed *
                           Mathf.Clamp(Input.GetAxis("Vertical"),0,1) *
                           Time.deltaTime, ForceMode.Acceleration);

        //TODO loop on edges of screen
        
        if (Input.GetButton("Shoot"))
        {
            if (Time.fixedTime - lastShot >= shootCooldown)
            {
                //TODO shoot prefab

                lastShot = Time.fixedTime;
            }
        }
    }
}
