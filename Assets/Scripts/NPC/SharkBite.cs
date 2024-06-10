using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBite : MonoBehaviour
{
    public float biteCooldown;
    public bool sharkBite;

    public Animator biteAnim;

    private bool waiting;

    public Damage damage;
    public EnemyPatrol enemyPatrol;


    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    IEnumerator SharkBiting()
    {
        waiting = true;
        sharkBite = true;
        biteAnim.SetBool("sharkBite", sharkBite);
        enemyPatrol.enabled = false;
        damage.enabled = false;
        yield return new WaitForSecondsRealtime(biteCooldown);
        damage.enabled = true;
        enemyPatrol.enabled = true;
        sharkBite = false;
        biteAnim.SetBool("sharkBite", sharkBite);
        waiting = false;
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (waiting)
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SharkBiting());
            
        }
    }
}
