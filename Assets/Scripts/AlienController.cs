using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlienController : MonoBehaviour
{
    public Transform player;
    public PlayerController playerCont;
    public Transform bulletHolder;
    public GameObject alienBulletPrefab;
    public Rigidbody rigidBody;
    
    public float shootCooldown = 1f;
    public float bulletOffset = 1.05f;
    
    public float moveCooldown = 10f;
    public float moveSpeed = 10f;
    public float collisionBounceForce = .75f;
    
    public float health = 20f;
    public float maxHealth = 20f;
    
    private float lastMove = -10f;
    private float lastShot = 0f;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerCont = player.GetComponent<PlayerController>();
        bulletHolder = GameObject.Find("BulletsController").GetComponent<Transform>();
    }

    
    void Update()
    {
        if (Time.fixedTime - lastMove > moveCooldown)
        {
            lastMove = Time.fixedTime;
            
            transform.rotation = Quaternion.identity;
            //reset rotation before angleing
            transform.Rotate(Vector3.up, Random.Range(0f,360f));
            //rotate in random direction around up axis
            rigidBody.velocity = Vector3.zero;
            rigidBody.AddRelativeForce(Vector3.forward * moveSpeed, ForceMode.VelocityChange);
            //stop all other movement, move forward in chosen direction for "alien like" movement
            
            //cant move and shoot at the same time :)
        }else if (Time.fixedTime - lastShot > shootCooldown && playerCont.health > 0)
        {
            lastShot = Time.fixedTime;

            GameObject bullet = Instantiate(alienBulletPrefab, transform.position,
                alienBulletPrefab.transform.rotation, bulletHolder);
            //make bullet at alien pos and bulletprefab rotation inside bulletholder (for nicer hierarchy)
            
            bullet.transform.LookAt(player, Vector3.up);
            bullet.transform.Rotate(Vector3.left, -90);
            //aim at player //add rotation because front of bullet model isnt front of object
            
            bullet.transform.Translate(Vector3.up * bulletOffset);
            //spawn in front of alien instead of inside

            bullet.GetComponent<BulletController>().inheritedVel = rigidBody.velocity;
            //add ships velocity to bullet
        }
        
        if (health <= 0) //if dead
        {
            playerCont.score += 25;
            //give score for killing alien
            
            Destroy(gameObject);
            
            //TODO death particle
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag) //switch case for tags
        {
            case "Asteroid":
                health -= 15f;
                
                Destroy(other.gameObject);
                //destroy obstacle
                
                rigidBody.velocity = Vector3.zero;
                rigidBody.AddForce((transform.position - other.gameObject.transform.position).normalized
                                   * collisionBounceForce, ForceMode.VelocityChange);
                //remove previous velocity, and pushed back with collisionBounceForce
                
                break;
            
            case "Bullet":
                health -= 7.5f;
                
                Destroy(other.gameObject);
                //destroy bullet
                
                //TODO got damage particle
                break;
            
            case "Player":
                health -= 10f;

                rigidBody.velocity = Vector3.zero;
                rigidBody.AddForce((transform.position - other.gameObject.transform.position).normalized
                                   * collisionBounceForce, ForceMode.VelocityChange);
                //remove previous velocity, and pushed back with collisionBounceForce
                
                //TODO bounce particle
                break;
            
            default:
                break;
        }
        //remove health based on what whas hit
    }
}
