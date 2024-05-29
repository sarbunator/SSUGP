using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotionSpawner : MonoBehaviour
{
    public Vector3[] spawnPoints; // Array of predefined spawn points
    public GameObject healingPotionPrefab; // The heal pot prefab
    public int maxActivePotions = 10; // Max number of active pots at a time
    public float spawnInterval = 5f; // Time interval between spawns

    private List<GameObject> activePotions = new List<GameObject>(); // List of currently active heal pots

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

        StartCoroutine(SpawnPotions());
    }

    IEnumerator SpawnPotions()
    {
        while (true)
        {
            if (activePotions.Count < maxActivePotions)
            {
                SpawnPotion();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnPotion()
    {
        Vector3 randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject spawnedPotion = Instantiate(healingPotionPrefab, randomSpawnPoint, Quaternion.identity);
        activePotions.Add(spawnedPotion);
    }
}