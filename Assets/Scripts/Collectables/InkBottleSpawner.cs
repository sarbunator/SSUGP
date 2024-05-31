using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkBottleSpawner : MonoBehaviour
{
    public Transform pos; // The position where objects will be spawned
    public Vector3[] spawnPoints; // Array of predefined spawn points
    public GameObject inkbottlePrefab; // The ink bottle prefab
    public int maxActiveBottles = 10; // Max number of active potions at a time
    public float spawnInterval = 5f; // Time interval between spawns
    private List<GameObject> activeBottles = new List<GameObject>(); // List of currently active ink bottles
    private bool isSpawningActive = false;

    void Start()
    {
        foreach (Vector3 spawnPoint in spawnPoints)  // Spawn a potion at each spawn point at the start
        {
            SpawnPotionAtPosition(spawnPoint);
        }
    }

    void Update()
    {
        activeBottles.RemoveAll(potion => potion == null);  // Clean up the activeBottles list by removing null references
        if (!isSpawningActive && activeBottles.Count < maxActiveBottles)
        {
            StartCoroutine(SpawnBottles());
        }
    }

    IEnumerator SpawnBottles()
    {
        isSpawningActive = true;
        while (activeBottles.Count < maxActiveBottles)
        {
            yield return new WaitUntil(() => activeBottles.Count < maxActiveBottles);    // wait until a bottle is collected before starting the timer
            yield return new WaitForSeconds(spawnInterval);  // wait for the spawn interval
            SpawnPotion(); // spawn a new bottle
        }
        isSpawningActive = false;
    }

    private void SpawnPotion()
    {
        Vector3 randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        SpawnPotionAtPosition(randomSpawnPoint);
    }

    private void SpawnPotionAtPosition(Vector3 position)
    {
        GameObject spawnedPotion = Instantiate(inkbottlePrefab, position, Quaternion.identity);
        activeBottles.Add(spawnedPotion);
    }
}