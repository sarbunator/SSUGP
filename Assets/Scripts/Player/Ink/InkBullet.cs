using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkBullet : MonoBehaviour
{

    public GameObject inkCloudPrefab;

 
    void ExplodeInk()
    {
        //isShooting = false;
        Instantiate(inkCloudPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        ExplodeInk();
    }
}
