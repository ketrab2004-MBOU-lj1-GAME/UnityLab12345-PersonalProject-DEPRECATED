using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        string tag = other.gameObject.tag;
        if (tag == "AlienBullet") //when shot by alien
        {
            Destroy(gameObject);
            //gone (but not alien bullet)
            
            //TODO Destroy Particles
        }else if (tag == "Bullet") //when shot by player
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().score += 10;
            //10 score when you shoot asteroid
            
            Destroy(gameObject);
            Destroy(other.gameObject);
            //gone and player bullet
            
            //TODO Destroy Particles
        }
    }
}
