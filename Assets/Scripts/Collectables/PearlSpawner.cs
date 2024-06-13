using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PearlSpawner : MonoBehaviour
{
    public GameObject[] objectsToInstantiate; // array to hold the pearl prefabs
    public float[] spawnWeights; // array to hold the spawn weights for each pearl

    public List<Transform> spawnPoints; // list to hold the predefined spawn points
    public float spawnInterval = 5f; 
    public int maxPearls = 10; 
    public float minDistFromPlayer = 5f;
    public float minDisteFromOtherPearls = 2f; // minimum distance from other pearls to spawn a new one
    public Transform playerTransform;
    public float spawnCheckRadius = 0.5f; // radius to check for existing pearls

    private List<Vector2> recentCollectionPositions = new List<Vector2>(); // list to store recent collection positions
    public float recentPositionCheckRadius = 3f; // radius to avoid recent collection positions
    public int recentPositionsToTrack = 5; // number of recent positions to track

    private void Start()
    {
        StartCoroutine(SpawnPearls()); // start the coroutine to spawn pearls
    }

    private IEnumerator SpawnPearls()
    {
        while (true) // infinite loop to keep spawning pearls
        {
            int currentPearlCount = GetTotalPearlCount(); // get the current count of all pearls in the scene

            if (currentPearlCount < maxPearls) // only spawn if the current count is below the max limit
            {
                for (int i = 0; i < spawnPoints.Count; i++) // iterate a number of times equal to the number of spawn points
                {
                    Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)]; // select a random spawn point

                    if (Vector2.Distance(spawnPoint.position, playerTransform.position) >= minDistFromPlayer) // check if the spawn point is far enough from the player
                    {
                        if (Physics2D.OverlapCircle(spawnPoint.position, spawnCheckRadius) == null) // check if there are no other pearls at this spawn point
                        {
                            if (!IsNearRecentCollection(spawnPoint.position)) // check if the spawn point is not near any recent collection positions
                            {
                                if (Random.value > 0.5f) // randomly decide whether to spawn a pearl at this spawn point (adjust this value to control the likelihood of spawning)
                                {
                                    if (currentPearlCount >= maxPearls) // stop spawning if we've reached the max limit
                                    {
                                        break; // exit the loop
                                    }
                                    SpawnRandomPearl(spawnPoint.position); // spawn a random pearl at the selected spawn point
                                    currentPearlCount++; // increment the count since we've spawned a pearl
                                }
                            }
                        }
                    }
                }
            }
            yield return new WaitForSeconds(spawnInterval); // wait for the specified interval before spawning again
        }
    }

    private int GetTotalPearlCount()
    {
        int count = 0; // initialize the count to zero
        count += GameObject.FindGameObjectsWithTag("Pearl_White").Length; // add the count of white pearls in the scene
        count += GameObject.FindGameObjectsWithTag("Pearl_Purple").Length; 
        count += GameObject.FindGameObjectsWithTag("Pearl_Golden").Length; 
        return count; // return the total count of pearls
    }

    private void SpawnRandomPearl(Vector2 spawnPosition)
    {
        float totalWeight = 0; // initialize the total weight to zero
        foreach (var weight in spawnWeights) // iterate through each weight
        {
            totalWeight += weight; // add the weight to the total weight
        }

        float randomValue = Random.Range(0f, totalWeight); // generate a random value between 0 and the total weight
        GameObject objectToSpawn = null; // initialize the object to spawn to null

        for (int i = 0; i < spawnWeights.Length; i++) // iterate through the spawn weights
        {
            if (randomValue < spawnWeights[i]) // check if the random value is less than the current weight
            {
                objectToSpawn = objectsToInstantiate[i]; // set the object to spawn to the corresponding pearl prefab
                break; // exit the loop
            }
            randomValue -= spawnWeights[i]; // subtract the current weight from the random value
        }

        if (objectToSpawn != null) // check if an object to spawn has been selected
        {
            GameObject spawnedPearl = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity); // instantiate the selected pearl prefab at the spawn position
            RegisterRecentCollection(spawnPosition); // register the spawn position as a recent collection position
        }
    }

    private bool IsNearRecentCollection(Vector2 position)
    {
        foreach (Vector2 recentPosition in recentCollectionPositions) // iterate through the recent collection positions
        {
            if (Vector2.Distance(position, recentPosition) < recentPositionCheckRadius) // check if the position is near any recent collection position
            {
                return true; // return true if the position is near a recent collection position
            }
        }
        return false; // return false if the position is not near any recent collection positions
    }

    private void RegisterRecentCollection(Vector2 position)
    {
        recentCollectionPositions.Add(position); // add the position to the recent collection positions list
        if (recentCollectionPositions.Count > recentPositionsToTrack) // check if the list size exceeds the number of recent positions to track
        {
            recentCollectionPositions.RemoveAt(0); // remove the oldest position to keep the list size manageable
        }
    }
}
