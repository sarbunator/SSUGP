using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSpawner : MonoBehaviour
{
    public GameObject healingPotionPrefab; // prefab for the healing potion
    public Transform[] spawnPoints; // array of predefined spawn points as Transforms
    public float spawnInterval = 5f; // time interval between spawns
    public int maxActivePotions = 10; // maximum number of active healing potions
    public float minDistanceFromPlayer = 5f; // minimum distance from the player for spawning
    public float minDistanceFromOtherPotions = 2f; // minimum distance from other potions for spawning
    public Transform playerTransform; // reference to the player's transform
    public float spawnCheckRadius = 0.5f; // radius to check for existing potions

    private List<GameObject> activePotions = new List<GameObject>(); // list of currently active healing potions

    private void Start()
    {
        // start the coroutine to manage potion spawning
        StartCoroutine(SpawnPotions());
    }

    private IEnumerator SpawnPotions()
    {
        while (true) // infinite loop to keep checking and spawning potions
        {
            activePotions.RemoveAll(potion => potion == null); // clean up the list by removing null references

            if (activePotions.Count < maxActivePotions) // spawn new potions if below the max limit
            {
                foreach (Transform spawnPoint in spawnPoints) // iterate through each spawn point
                {
                    if (activePotions.Count >= maxActivePotions) break; // stop if max limit is reached

                    // check if the spawn point is far enough from the player and other potions
                    if (Vector3.Distance(spawnPoint.position, playerTransform.position) >= minDistanceFromPlayer &&
                        !Physics2D.OverlapCircle(spawnPoint.position, spawnCheckRadius) &&
                        !IsNearOtherPotions(spawnPoint.position))
                    {
                        SpawnPotionAtPosition(spawnPoint.position); // spawn the potion
                    }
                }
            }

            yield return new WaitForSeconds(spawnInterval); // wait for the next spawn interval
        }
    }

    private void SpawnPotionAtPosition(Vector3 position)
    {
        // instantiate a new potion at the specified position and add it to the active potions list
        GameObject spawnedPotion = Instantiate(healingPotionPrefab, position, Quaternion.identity);
        activePotions.Add(spawnedPotion);
    }

    private bool IsNearOtherPotions(Vector3 position)
    {
        // check if the position is near any existing active potions
        foreach (GameObject potion in activePotions)
        {
            if (Vector3.Distance(position, potion.transform.position) < minDistanceFromOtherPotions)
            {
                return true;
            }
        }
        return false;
    }
}
