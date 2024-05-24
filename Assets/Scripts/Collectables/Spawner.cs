using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform pos;
    public GameObject[] objectsToInstantiate;


    void Start()
    {
        InstantiateObject();
    }
    
    private void InstantiateObject()

    {
        int n = Random.Range(0, objectsToInstantiate.Length);

        GameObject g = Instantiate(objectsToInstantiate[n], pos.position, objectsToInstantiate[n].transform.rotation);

    }
}

 