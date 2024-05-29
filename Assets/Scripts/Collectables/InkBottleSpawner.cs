using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Vector3[] spawnPoints; // Array of predefined spawn points (positions)
    public GameObject objectToSpawn; // The object to spawn
    public int maxActiveObjects = 5; // Maximum number of active objects at a time
    public float spawnInterval = 5f; // Time interval between spawns

    private List<GameObject> activeObjects = new List<GameObject>(); // List of currently active objects

    void Start()
    {
        if (spawnPoints.Length != 10)
        {
            Debug.LogError("There should be exactly 10 spawn points.");
            return;
        }

        if (objectToSpawn == null)
        {
            Debug.LogError("Object to spawn is not assigned.");
            return;
        }

        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            if (activeObjects.Count < maxActiveObjects)
            {
                SpawnObject();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnObject()
    {
        Vector3 randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject spawnedObject = Instantiate(objectToSpawn, randomSpawnPoint, Quaternion.identity);
        activeObjects.Add(spawnedObject);

        // Subscribe to the object's destruction event
        DestroyableObject destroyable = spawnedObject.GetComponent<DestroyableObject>();
        if (destroyable != null)
        {
            destroyable.OnDestroyed += () => HandleObjectDestroyed(spawnedObject);
        }
    }

    private void HandleObjectDestroyed(GameObject destroyedObject)
    {
        activeObjects.Remove(destroyedObject);
    }
}


