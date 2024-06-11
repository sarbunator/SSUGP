using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlSpawner : MonoBehaviour
{
    public GameObject[] objectsToInstantiate; // Array to hold the pearl prefabs
    public float[] spawnWeights; // Array to hold the spawn weights for each pearl

    public List<Transform> spawnPoints; // List to hold the predefined spawn points
    public float spawnInterval = 5f; // Time in seconds between spawns

    private void Start()
    {
        // Start the coroutine that handles pearl spawning
        StartCoroutine(SpawnPearls());
    }

    private IEnumerator SpawnPearls()
    {
        while (true) // Infinite loop to keep spawning pearls
        {
            // Loop through each spawn point and spawn a pearl
            foreach (Transform spawnPoint in spawnPoints)
            {
                SpawnRandomPearl(spawnPoint.position);
            }
            // Wait for the specified interval before spawning again
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnRandomPearl(Vector2 spawnPosition)
    {
        float totalWeight = 0;
        // Calculate the total weight by summing up all spawn weights
        foreach (var weight in spawnWeights)
        {
            totalWeight += weight;
        }

        // Generate a random value between 0 and the total weight
        float randomValue = Random.Range(0f, totalWeight);
        GameObject objectToSpawn = null;

        // Determine which pearl to spawn based on the random value and weights
        for (int i = 0; i < spawnWeights.Length; i++)
        {
            if (randomValue < spawnWeights[i])
            {
                // Select the corresponding pearl prefab to spawn
                objectToSpawn = objectsToInstantiate[i];
                break;
            }
            // Subtract the current weight from the random value for the next iteration
            randomValue -= spawnWeights[i];
        }

        // Instantiate the selected pearl at the specified spawn position
        if (objectToSpawn != null)
        {
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
