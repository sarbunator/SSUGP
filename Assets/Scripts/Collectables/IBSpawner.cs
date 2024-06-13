using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBSpawner : MonoBehaviour
{
    public GameObject inkBottlePrefab; // prefab for the ink bottle
    public Transform[] spawnPoints; // array of predefined spawn points as Transforms
    public float spawnInterval = 5f; // time interval between spawns
    public int maxActiveBottles = 10; // maximum number of active ink bottles
    public float minDistanceFromPlayer = 5f; // minimum distance from the player for spawning
    public float minDistanceFromOtherBottles = 2f; // minimum distance from other bottles for spawning
    public Transform playerTransform; // reference to the player's transform
    public float spawnCheckRadius = 0.5f; // radius to check for existing bottles

    private List<GameObject> activeBottles = new List<GameObject>(); // list of currently active ink bottles

    private void Start()
    {
        // start the coroutine to manage bottle spawning
        StartCoroutine(SpawnBottles());
    }

    private IEnumerator SpawnBottles()
    {
        while (true) // infinite loop to keep checking and spawning bottles
        {
            activeBottles.RemoveAll(bottle => bottle == null); // clean up the list by removing null references

            if (activeBottles.Count < maxActiveBottles) // spawn new bottles if below the max limit
            {
                foreach (Transform spawnPoint in spawnPoints) // iterate through each spawn point
                {
                    if (activeBottles.Count >= maxActiveBottles) break; // stop if max limit is reached

                    // check if the spawn point is far enough from the player and other bottles
                    if (Vector3.Distance(spawnPoint.position, playerTransform.position) >= minDistanceFromPlayer &&
                        !Physics2D.OverlapCircle(spawnPoint.position, spawnCheckRadius) &&
                        !IsNearOtherBottles(spawnPoint.position))
                    {
                        SpawnBottleAtPosition(spawnPoint.position); // spawn the bottle
                    }
                }
            }

            yield return new WaitForSeconds(spawnInterval); // wait for the next spawn interval
        }
    }

    private void SpawnBottleAtPosition(Vector3 position)
    {
        // instantiate a new bottle at the specified position and add it to the active bottles list
        GameObject spawnedBottle = Instantiate(inkBottlePrefab, position, Quaternion.identity);
        activeBottles.Add(spawnedBottle);
    }

    private bool IsNearOtherBottles(Vector3 position)
    {
        // check if the position is near any existing active bottles
        foreach (GameObject bottle in activeBottles)
        {
            if (Vector3.Distance(position, bottle.transform.position) < minDistanceFromOtherBottles)
            {
                return true;
            }
        }
        return false;
    }
}
