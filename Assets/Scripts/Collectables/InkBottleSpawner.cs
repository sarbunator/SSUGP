using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkBottleSpawner : MonoBehaviour
{
    public Transform pos;
    public Vector3[] spawnPoints;
    public GameObject inkbottlePrefab;
    public int maxActiveBottles = 10;
    public float spawnInterval = 5f;
    private List<GameObject> activeBottles = new List<GameObject>();
    private bool isSpawningActive = false;

    void Start()
    {
        foreach (Vector3 spawnPoint in spawnPoints) 
        {
            SpawnPotionAtPosition(spawnPoint);
        }
    }

    void Update()
    {
        activeBottles.RemoveAll(potion => potion == null); 
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