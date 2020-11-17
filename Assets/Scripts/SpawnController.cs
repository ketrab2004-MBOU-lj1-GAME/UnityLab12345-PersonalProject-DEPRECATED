using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    public GameObject[] alienPrefabArray;
    public GameObject[] asteroidPrefabArray;
    
    readonly float gameWidth = 5f*1.78f +.5f;
    readonly float gameHeight = 5f +.5f;
    readonly float gameY = 0f;
    
    readonly float spawnObstacleSpeed = 1f;

    public int waveCount = 0;
    private bool summoningWave = true;

    // Update is called once per frame
    void Start()
    {
        Invoke("SummonWave", 2.5f); //spawn first wave after 2.5f
    }

    private void Update()
    {
        if (!summoningWave && transform.childCount <= 4) //if not already summoning wave and <= 4 enemies left
        {
            summoningWave = true; //set to true so it doesn't summon hundreds of waves in mere seconds
            Invoke("SummonWave", 5f); //summon wave after a bit so it looks more "natural"
        }
    }

    void SummonWave()
    {
        int alienCount = Mathf.RoundToInt(Mathf.Max(0, -3 + waveCount / 2));
        //calculate amount of aliens
        int asteroidCount = Mathf.Max(2,8 + Mathf.RoundToInt(waveCount / 5) -alienCount);
        //calculate amount of asteroids, and reduce to make space for aliens

        for (int i = 0; i < asteroidCount; i++)
        {
            SpawnAsteroid(); //loop to spawn asteroids
        }
        for (int i = 0; i < alienCount; i++)
        {
            SpawnAlien(); //loop to spawn aliens
        }

        summoningWave = false; //set summoning wave to false so next wave can spawn
        waveCount++; //increase wave
    }
    
    
    void SpawnAsteroid()
    {
        GameObject obstacle = Instantiate(asteroidPrefabArray[Random.Range(0, asteroidPrefabArray.Length - 1)], GenSpawnLocation(),
            Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up), transform); //gen asteroid

        float scaleChange = Random.Range(-.2f, .2f);
        obstacle.transform.localScale += new Vector3(scaleChange, scaleChange, scaleChange);
        //add random size to asteroid
        
        obstacle.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * spawnObstacleSpeed, ForceMode.VelocityChange);
        //add movement to asteroid
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
