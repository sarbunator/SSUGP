using System.Collections;
using UnityEngine;

public class TestPearlSpawner : MonoBehaviour
{
    public Transform pos; // The position where objects will be spawned
    public GameObject[] objectsToInstantiate; // Array of objects to spawn
    public float[] spawnWeights; // Array of weights for each object
    public float minSpawnInterval = 1f; // Minimum time between spawns
    public float maxSpawnInterval = 3f; // Maximum time between spawns
    public Vector3 spawnRange; // Range within which to randomly spawn objects
    public float minDistanceFromLastSpawn = 5f; // Minimum distance from the last spawn position
    public Transform playerTransform; // Player's Transform
    public float minDistanceFromPlayer = 5f; // Minimum distance from the player

    private Vector3 lastSpawnPosition;
    private float[] cumulativeWeights;

    void Start()
    {
        if (pos == null)
        {
            Debug.LogError("Position Transform not assigned.");
            return;
        }

        if (objectsToInstantiate == null || objectsToInstantiate.Length == 0)
        {
            Debug.LogError("Objects to instantiate not assigned or empty.");
            return;
        }

        if (spawnWeights == null || spawnWeights.Length != objectsToInstantiate.Length)
        {
            Debug.LogError("Spawn weights not assigned or not matching the number of objects.");
            return;
        }

        if (playerTransform == null)
        {
            Debug.LogError("Player Transform not assigned.");
            return;
        }

        if (!ValidateWeights())
        {
            Debug.LogError("Invalid spawn weights.");
            return;
        }

        CalculateCumulativeWeights();
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            InstantiateObject();
            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    private void InstantiateObject()
    {
        int selectedIndex = GetWeightedRandomIndex();

        Vector3 spawnPosition;
        int attempt = 0;
        do
        {
            if (attempt++ > 100)
            {
                Debug.LogWarning("Could not find a suitable spawn position after 100 attempts.");
                return;
            }

            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnRange.x, spawnRange.x),
                Random.Range(-spawnRange.y, spawnRange.y),
                Random.Range(-spawnRange.z, spawnRange.z)
            );

            spawnPosition = pos.position + randomOffset;

        } while (Vector3.Distance(spawnPosition, lastSpawnPosition) < minDistanceFromLastSpawn ||
                 Vector3.Distance(spawnPosition, playerTransform.position) < minDistanceFromPlayer);

        lastSpawnPosition = spawnPosition;
        Instantiate(objectsToInstantiate[selectedIndex], spawnPosition, objectsToInstantiate[selectedIndex].transform.rotation);
    }

    private int GetWeightedRandomIndex()
    {
        float randomValue = Random.Range(0, cumulativeWeights[cumulativeWeights.Length - 1]);

        for (int i = 0; i < cumulativeWeights.Length; i++)
        {
            if (randomValue < cumulativeWeights[i])
            {
                return i;
            }
        }

        // Fallback, should not reach here if weights are positive and valid
        return 0;
    }

    private bool ValidateWeights()
    {
        foreach (float weight in spawnWeights)
        {
            if (weight < 0)
            {
                return false;
            }
        }
        return true;
    }

    private void CalculateCumulativeWeights()
    {
        cumulativeWeights = new float[spawnWeights.Length];
        cumulativeWeights[0] = spawnWeights[0];

        for (int i = 1; i < spawnWeights.Length; i++)
        {
            cumulativeWeights[i] = cumulativeWeights[i - 1] + spawnWeights[i];
        }
    }
}