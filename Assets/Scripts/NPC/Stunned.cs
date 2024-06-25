using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunned : MonoBehaviour
{
    public float stunDuration;
    public float stunDurationSecondary;
    [HideInInspector]
    public bool isStunned;

    public GameObject stunEffect;

    public EnemyPatrol enemyPatrol;
    public Damage damage;
    private bool waiting;



    IEnumerator Stun()
    {
        waiting = true;
        isStunned = true;
        stunEffect.SetActive(true);
        enemyPatrol.enabled = false;
        damage.enabled = false;
        yield return new WaitForSeconds(stunDuration);
        enemyPatrol.enabled = true;
        damage.enabled = true;
        isStunned = false;
        stunEffect.SetActive(false);
        waiting = false;
    }

    IEnumerator StunSecondary()
    {
        waiting = true;
        isStunned = true;
        stunEffect.SetActive(true);
        enemyPatrol.enabled = false;
        yield return new WaitForSeconds(stunDurationSecondary);
        enemyPatrol.enabled = true;
        isStunned = false;
        stunEffect.SetActive(false);
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
        if (collision.gameObject.CompareTag("InkSplashSecondary"))
        {
            StartCoroutine(StunSecondary());
        }
    }

}
