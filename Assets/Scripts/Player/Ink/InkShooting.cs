using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkShooting : MonoBehaviour
{

    public GameObject inkProjectilePrefab;
    public GameObject inkCloudPrefab;
    public float projectileSpeed;  
    public float maxProjectileDistance;

    public GameObject inkSecondaryFireCloudPrefab;

    private GameObject inkBullet;
    private Vector2 shootDirection;
    private bool isShooting = false;
    private Vector2 playerPosition;


    void ShootInk()
    {
        isShooting = true;

        inkBullet = Instantiate(inkProjectilePrefab, transform.position, Quaternion.identity);
        playerPosition = inkBullet.transform.position;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        shootDirection = ((Vector2)(mousePosition - transform.position)).normalized;
    }
 
    void ShootingControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootInk();
        }
        if (Input.GetMouseButtonUp(0) && isShooting)
        {
            ExplodeInk();
        }

        if (isShooting && inkBullet != null) // if the ink exploded and there is no inkbullet. (Here is added the possible ammo count condition check) ********************** via GameManager?
        {
            inkBullet.transform.position +=(Vector3)(projectileSpeed * Time.deltaTime * shootDirection);

            if(Vector2.Distance(playerPosition, inkBullet.transform.position) >= maxProjectileDistance)
            {
                ExplodeInk(); // If the ink flies too far it automatically explodes.
            }
        }

        if (Input.GetMouseButtonDown(1)) // Need to add more here. Cooldown etc.
        {
            Instantiate(inkSecondaryFireCloudPrefab, transform.position, transform.rotation);
        }
    }

    void ExplodeInk()
    {
        isShooting = false;
        
        if(inkBullet != null)
        {
            Instantiate(inkCloudPrefab, inkBullet.transform.position, Quaternion.identity);

            Destroy(inkBullet);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            print("osuu kait");
            ExplodeInk();
        }
    }

    void Update()
    {
        ShootingControls();
    }
}
