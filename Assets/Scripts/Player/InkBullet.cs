using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkBullet : MonoBehaviour
{

    public GameObject inkCloudPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
