using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    readonly float rocketSpeed = 50f;
    readonly float turnSpeed = 180f;
    readonly float collisionBounceForce = 1f;
    readonly float shootKnockback = .05f;
    readonly float ownBulletKnockMult = .1f;
    
    readonly float shootCooldown = .25f;
    private float lastShot = -1f;
    readonly float bulletOffset = -.35f;

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
        //all movement in function

        if (Input.GetButton("Shoot"))
        {
            shoot(); //shoot when "Shoot" button pressed
            //TODO shoot particle
        }

        if (gameObject.activeSelf) //not dead
        {
            score += Time.deltaTime; //increase score slowly when alive
        }
        if (health <= 0)
        {
            gameObject.SetActive(false); //deactivate when dead
            
            print("Game Over: " + score); //print score
            
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
        
        //TODO rocket particles
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
        if (Time.fixedTime - lastShot >= shootCooldown) //if enough time passed since lastShot do
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation, bulletHolder.transform);
            bullet.transform.Translate(transform.forward * bulletOffset);
            //make bullet and set in right pos

            bullet.GetComponent<BulletController>().inheritedVel = rigidBody.velocity;
            //set bullet inherited velocity to source's (player) velocity
            
            rigidBody.AddForce(-transform.up * shootKnockback, ForceMode.Impulse);
            //add knockback to player when shooting

            lastShot = Time.fixedTime;
            //print(lastShot);
        }
    }
}