using System.Collections;
using UnityEngine;

public class InkBottleSpawner : MonoBehaviour
{
    public Transform pos; // The position where objects will be spawned
    public GameObject[] objectsToInstantiate; // Array of objects to spawn
    public float[] spawnWeights; // Array of weights for each object
    public float minSpawnInterval = 1f; // Minimum time between spawns
    public float maxSpawnInterval = 3f; // Maximum time between spawns
    public Vector3 spawnRange; // Range within which to randomly spawn objects

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
        if (objectsToInstantiate == null || objectsToInstantiate.Length == 0)
        {
            Debug.LogError("Objects to instantiate array is null or empty during runtime.");
            return;
        }

        int selectedIndex = GetWeightedRandomIndex();

        if (objectsToInstantiate[selectedIndex] == null)
        {
            Debug.LogError($"Object at index {selectedIndex} is null.");
            return;
        }

        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnRange.x, spawnRange.x),
            Random.Range(-spawnRange.y, spawnRange.y),
            Random.Range(-spawnRange.z, spawnRange.z)
        );

        Vector3 spawnPosition = pos.position + randomOffset;
        Instantiate(objectsToInstantiate[selectedIndex], spawnPosition, objectsToInstantiate[selectedIndex].transform.rotation);
    }

    private int GetWeightedRandomIndex()
    {
        float totalWeight = 0;
        for (int i = 0; i < spawnWeights.Length; i++)
        {
            totalWeight += spawnWeights[i];
        }

        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0;

        for (int i = 0; i < spawnWeights.Length; i++)
        {
            cumulativeWeight += spawnWeights[i];
            if (randomValue < cumulativeWeight)
            {
                return i;
            }
        }

        // Fallback, should not reach here if weights are positive
        return 0;
    }
}