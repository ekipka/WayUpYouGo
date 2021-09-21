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
    
    private Vector3 platformSpawnPosition;

    void Start()
    {
        platformSpawnPosition = transform.position;
        InvokeRepeating(nameof(SpawnPlatform), 2, 1);
    }

    public void SpawnPlatform()
    {
        GameObject newPlatform = Instantiate(platformPrefab, platformSpawnPosition, Quaternion.identity);
        
        float randomDistance = RandomiseNumber(minPlatformVerticalDistance, maxPlatformVerticalDistance);
        float randomSize = RandomiseNumber(minPlatformLength, maxPlatformLength);
        
        platformSpawnPosition.y = transform.position.y + randomDistance;
        newPlatform.transform.localScale = new Vector3(randomSize, 0.12f, 1f);  // TODO podmienic statyczne wartosci na pobierane z edytora
    }
    
    private static float RandomiseNumber(float min, float max)
    {
        var random = new System.Random();
        return (float)random.NextDouble() * (max - min) + min;
    }
}
