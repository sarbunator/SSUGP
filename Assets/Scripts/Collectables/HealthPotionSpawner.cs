using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotionSpawner : MonoBehaviour
{
    public Transform pos; // The position where objects will be spawned
    public Vector3[] spawnPoints; // Array of predefined spawn points
    public GameObject healingPotionPrefab; // The healing potion prefab
    public int maxActivePotions = 10; // Max number of active potions at a time
    public float spawnInterval = 5f; // Time interval between spawns

    private List<GameObject> activePotions = new List<GameObject>(); // List of currently active healing potions
    private bool isSpawningActive = false;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned.");
            return;
        }

        if (healingPotionPrefab == null)
        {
            Debug.LogError("Healing potion prefab not assigned.");
            return;
        }

        // Spawn a potion at each spawn point at the start
        foreach (Vector3 spawnPoint in spawnPoints)
        {
            SpawnPotionAtPosition(spawnPoint);
        }
    }

    void Update()
    {
        // Clean up the activePotions list by removing null references
        activePotions.RemoveAll(potion => potion == null);

        if (!isSpawningActive && activePotions.Count < maxActivePotions)
        {
            StartCoroutine(SpawnPotions());
        }
    }

    IEnumerator SpawnPotions()
    {
        isSpawningActive = true;

        while (activePotions.Count < maxActivePotions)
        {
            // Wait until a potion is collected before starting the timer
            yield return new WaitUntil(() => activePotions.Count < maxActivePotions);

            // Wait for the spawn interval
            yield return new WaitForSeconds(spawnInterval);

            // Spawn a new potion
            SpawnPotion();
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
        GameObject spawnedPotion = Instantiate(healingPotionPrefab, position, Quaternion.identity);
        activePotions.Add(spawnedPotion);
    }
}