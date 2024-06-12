using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlSpawner : MonoBehaviour
{
    public GameObject[] objectsToInstantiate; // Array to hold the pearl prefabs
    public float[] spawnWeights; // Array to hold the spawn weights for each pearl

    public List<Transform> spawnPoints; // List to hold the predefined spawn points
    public float spawnInterval = 5f; // Time in seconds between spawns
    public int maxPearls = 10; // Maximum number of pearls allowed in the scene at one time
    public float minDistFromPlayer = 5f; // Minimum distance from the player to spawn pearls
    public float minDistFromOtherPearls = 10f; // Minimum distance from other pearls to spawn a new one
    public Transform playerTransform; // Reference to the player's Transform
    public float spawnCheckRadius = 0.5f; // Radius to check for existing pearls

    private List<Vector2> recentCollectionPositions = new List<Vector2>(); // List to store recent collection positions
    public float recentPositionCheckRadius = 3f; // Radius to avoid recent collection positions
    public int recentPositionsToTrack = 5; // Number of recent positions to track

    private void Start()
    {
        StartCoroutine(SpawnPearls());
    }

    private IEnumerator SpawnPearls()
    {
        while (true) // Infinite loop to keep spawning pearls
        {
            // Get the current count of all pearls in the scene
            int currentPearlCount = GetTotalPearlCount();

            if (currentPearlCount < maxPearls) // Only spawn if the current count is below the max limit
            {
                for (int i = 0; i < spawnPoints.Count; i++)
                {
                    // Select a random spawn point
                    Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

                    // Check if the spawn point is far enough from the player
                    if (Vector2.Distance(spawnPoint.position, playerTransform.position) >= minDistFromPlayer)
                    {
                        // Check if there are no other pearls at this spawn point
                        if (Physics2D.OverlapCircle(spawnPoint.position, spawnCheckRadius) == null)
                        {
                            // Check if the spawn point is not near any recent collection positions
                            if (!IsNearRecentCollection(spawnPoint.position))
                            {
                                // Randomly decide whether to spawn a pearl at this spawn point
                                if (Random.value > 0.5f) // Adjust this value to control the likelihood of spawning
                                {
                                    if (currentPearlCount >= maxPearls)
                                    {
                                        break; // Stop spawning if we've reached the max limit
                                    }
                                    SpawnRandomPearl(spawnPoint.position);
                                    currentPearlCount++; // Increment the count since we've spawned a pearl
                                }
                            }
                        }
                    }
                }
            }
            // Wait for the specified interval before spawning again
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private int GetTotalPearlCount()
    {
        int count = 0;
        count += GameObject.FindGameObjectsWithTag("Pearl_White").Length;
        count += GameObject.FindGameObjectsWithTag("Pearl_Purple").Length;
        count += GameObject.FindGameObjectsWithTag("Pearl_Golden").Length;
        return count;
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

        for (int i = 0; i < spawnWeights.Length; i++)
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
            GameObject spawnedPearl = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            RegisterRecentCollection(spawnPosition); // Register the spawn position as a recent collection position
        }
    }

    private bool IsNearRecentCollection(Vector2 position)
    {
        foreach (Vector2 recentPosition in recentCollectionPositions)
        {
            if (Vector2.Distance(position, recentPosition) < recentPositionCheckRadius)
            {
                return true;
            }
        }
        return false;
    }

    private void RegisterRecentCollection(Vector2 position)
    {
        recentCollectionPositions.Add(position);
        if (recentCollectionPositions.Count > recentPositionsToTrack)
        {
            recentCollectionPositions.RemoveAt(0); // Keep the list size manageable
        }
    }
}
