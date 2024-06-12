using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlSpawner : MonoBehaviour
{
    public GameObject[] objectsToInstantiate; 
    public float[] spawnWeights; 

    public List<Transform> spawnPoints;
    public float spawnInterval = 5f; 

    private void Start()
    {
        StartCoroutine(SpawnPearls());
    }

    private IEnumerator SpawnPearls()
    {
        while (true) 
        {
            foreach (Transform spawnPoint in spawnPoints) // loop through each spawn pouint and spawn a pearl
            {
                SpawnRandomPearl(spawnPoint.position);
            }
            yield return new WaitForSeconds(spawnInterval); // interval before spawning again
        }
    }

    private void SpawnRandomPearl(Vector2 spawnPosition)
    {
        float totalWeight = 0;  
        foreach (var weight in spawnWeights)
        {
            totalWeight += weight;
        }

        float randomValue = Random.Range(0f, totalWeight);
        GameObject objectToSpawn = null;

        for (int i = 0; i < spawnWeights.Length; i++) // which pearl to spawn based on the random value and weights
        {
            if (randomValue < spawnWeights[i])
            {
                objectToSpawn = objectsToInstantiate[i];
                break;
            }
            randomValue -= spawnWeights[i];
        }

        if (objectToSpawn != null)
        {
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
