using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavePearlSpawner : MonoBehaviour
{
    public GameObject whitePearlPrefab;
    public GameObject purplePearlPrefab;
    public GameObject goldenPearlPrefab;

    public List<Transform> spawnPoints;
    public List<string> spawnTypes; // List to define the type of pearl at each spawn point

    private void Start()
    {
        SpawnPearls();
    }

    private void SpawnPearls()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (i < spawnTypes.Count)
            {
                SpawnPearl(spawnPoints[i].position, spawnTypes[i]);
            }
        }
    }

    private void SpawnPearl(Vector2 spawnPosition, string pearlType)
    {
        GameObject pearlToSpawn = null;

        switch (pearlType.ToLower())
        {
            case "white":
                pearlToSpawn = whitePearlPrefab;
                break;
            case "purple":
                pearlToSpawn = purplePearlPrefab;
                break;
            case "golden":
                pearlToSpawn = goldenPearlPrefab;
                break;
            default:
                Debug.LogError("Invalid pearl type specified: " + pearlType);
                return;
        }

        Instantiate(pearlToSpawn, spawnPosition, Quaternion.identity);
    }
}
