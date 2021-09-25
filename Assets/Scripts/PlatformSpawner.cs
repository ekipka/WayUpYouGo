using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public List<Texture2D> textures = new List<Texture2D>();  // TODO pozniej dodac losujace sie teksturki
    
    public float minPlatformLength;
    public float maxPlatformLength;
    public float minPlatformVerticalDistance;
    public float maxPlatformVerticalDistance;

    // max left and right platform spawn coordinates
    private float wallsDistanceL = -1.4f;
    private float wallsDistanceR = 1.4f; 

    void Start()
    {
        InvokeRepeating(nameof(SpawnPlatform), 2, 1);
    }

    public void SpawnPlatform()
    {
        float randomVerticalDistance = RandomiseNumber(minPlatformVerticalDistance, maxPlatformVerticalDistance);
        float randomHorizontalDistance = RandomiseNumber(wallsDistanceL, wallsDistanceR);
        float randomSize = RandomiseNumber(minPlatformLength, maxPlatformLength);
       
        transform.position = new Vector3(transform.localPosition.x + randomHorizontalDistance, 
            transform.localPosition.y + randomVerticalDistance, transform.localPosition.z);

        GameObject newPlatform = Instantiate(platformPrefab,  transform.position, Quaternion.identity);
        newPlatform.transform.localScale = new Vector3(randomSize, 0.12f, 1f);  // TODO podmienic statyczne wartosci na pobierane z edytora
    }
    
    private static float RandomiseNumber(float min, float max)
    {
        var random = new System.Random();
        return (float)random.NextDouble() * (max - min) + min;
    }
}
