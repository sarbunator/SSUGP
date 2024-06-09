using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunned : MonoBehaviour
{
    public float stunDuration;
    public bool isStunned;

    public EnemyPatrol enemyPatrol;
    private bool waiting;



    IEnumerator Stun()
    {
        waiting = true;
        isStunned = true;
        enemyPatrol.enabled = false;
        yield return new WaitForSeconds(stunDuration);
        enemyPatrol.enabled = true;
        isStunned = false;
        waiting = false;
    }


    void Start()
    {
        
    }


    void Update()
    {
        
    }
    

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (waiting)
    //        return;
    //    if (collision.gameObject.CompareTag("InkSplashMain"))
    //    {
    //        StartCoroutine(Stun());
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (waiting) 
            return;
        if (collision.gameObject.CompareTag("InkSplashMain"))
        {
            StartCoroutine(Stun());
        }
    }

}
