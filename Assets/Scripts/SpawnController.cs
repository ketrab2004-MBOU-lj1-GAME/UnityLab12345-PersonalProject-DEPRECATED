using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] alienPrefabArray;
    public GameObject[] asteroidPrefabArray;
    
    private float gameWidth = 5f*1.78f + .5f;
    private float gameHeight = 5f + .5f;
    private float gameY = 0f;
    
    private float spawnObstacleSpeed = 5f;

    private int waveCount = 1;

    // Update is called once per frame
    void Start()
    {
        Invoke("SummonWave", 15);
    }

    void SummonWave()
    {
        
        
        waveCount++;
        Invoke("SummonWave", 20);
    }
    
    void SpawnAsteroid()
    {
        GameObject obstacle = Instantiate(asteroidPrefabArray[Random.Range(0, asteroidPrefabArray.Length - 1)], GenSpawnLocation(),
            Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up), transform); //gen asteroid
        
        obstacle.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * spawnObstacleSpeed, ForceMode.VelocityChange);
    }

    void SpawnAlien()
    {
        GameObject obstacle = Instantiate(alienPrefabArray[Random.Range(0, alienPrefabArray.Length - 1)], GenSpawnLocation(),
            Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up), transform); //gen alien
    }

    Vector3 GenSpawnLocation()
    {
        if (Random.Range(0, 2) == 1) //gen left/right (max is exclusive)
        {
            return new Vector3((Random.Range(0,2)-.5f)*gameWidth*2, gameY, Random.Range(-1f,1f)*gameHeight);
        }
        else //gen top/bottom
        {
            return new Vector3(Random.Range(-1f,1f)*gameWidth, gameY, (Random.Range(0,2)-.5f)*gameHeight*2);
        }
    }
}
