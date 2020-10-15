using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rocketSpeed = 50f;
    public float turnSpeed = 180f;
    public float collisionBounceForce = 1f;
    
    public float shootCooldown = .1f;
    private float lastShot = -1f;
    public float bulletOffset = 1f;

    public float health = 100f;
    public float maxHealth = 100f;
    public float score = 0f;
    
    public GameObject bulletPrefab;
    public GameObject bulletHolder;
    public Rigidbody rigidBody;

    // Update is called once per frame
    void Update()
    {
        movePlayer();

        if (Input.GetButton("Shoot"))
        {
            shoot();
            //TODO shoot particle
        }

        if (gameObject.activeSelf) //not dead
        {
            score += Time.deltaTime;
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);
            
            print("Game Over:" + score);
            
            //TODO death and particle
        }
    }

    void movePlayer()
    {
        //Rotate
        transform.Rotate(Vector3.forward, turnSpeed * Mathf.Pow(-Input.GetAxis("Horizontal"),3) * Time.deltaTime);
        //Propulsion
        rigidBody.AddForce(transform.up * rocketSpeed *
                           Mathf.Clamp(Input.GetAxis("Vertical"),0,1) *
                           Time.deltaTime, ForceMode.Acceleration);
        
        //TODO propulsion when vertical input
    }
    
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag) //switch case for tags
        {
            case "Asteroid":
                health -= 20f;

                Destroy(other.gameObject);
                    //destroy obstacle
                
                rigidBody.velocity = Vector3.zero;
                rigidBody.AddForce((transform.position - other.gameObject.transform.position).normalized
                                   * collisionBounceForce, ForceMode.VelocityChange);
                //remove previous velocity, and pushed back with collisionBounceForce
                
                break;
            
            case "AlienBullet":
                health -= 10f;
                
                Destroy(other.gameObject);
                //destroy bullet
                
                break;
            
            case "Alien":
                health -= 25f;
                
                rigidBody.velocity = Vector3.zero;
                rigidBody.AddForce((transform.position - other.gameObject.transform.position).normalized
                                   * collisionBounceForce, ForceMode.VelocityChange);
                //remove previous velocity, and pushed back with collisionBounceForce
                
                break;
            
            default:
                break;
        }
        //remove health based on what whas hit
    }
    
    void shoot()
    {
        if (Time.fixedTime - lastShot >= shootCooldown)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation, bulletHolder.transform);
            bullet.transform.Translate(transform.forward * bulletOffset);

            lastShot = Time.fixedTime;
            print(lastShot);
        }
    }
}