using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform pos;
    public GameObject[] objectsToInstantiate;
    public float minSpawnInterval = 5f; // Minimum time between spawns
    public float maxSpawnInterval = 10f; // Maximum time between spawns

    void Start()
    {
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
        int n = Random.Range(0, objectsToInstantiate.Length);

        GameObject g = Instantiate(objectsToInstantiate[n], pos.position, objectsToInstantiate[n].transform.rotation);

    }
}

 