using System.Collections;
using UnityEngine;

public class TestPearlSpawner : MonoBehaviour
{
    public Transform pos; // position where objects will be spawned
    public GameObject[] objectsToInstantiate;
    public float[] spawnWeights;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;
    public Vector3 spawnRange; // range within which to randomly spawn objects
    public float minDistanceFromLastSpawn = 5f;
    public Transform playerTransform;
    public float minDistanceFromPlayer = 5f;

    private Vector3 lastSpawnPosition;
    private float[] cumulativeWeights;

    void Start()
    {
        if (pos == null || objectsToInstantiate == null || objectsToInstantiate.Length == 0 ||
            spawnWeights == null || spawnWeights.Length != objectsToInstantiate.Length ||
            playerTransform == null || !ValidateWeights())
        {
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