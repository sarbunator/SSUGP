using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public PlayerHealth pHealth;
    public float damage;

    public EyeMechanics eyeMechanics;

    public bool isDamaged;
    public float damageGraceTime;



    void Start()
    {
        
    }

    IEnumerator TookDamage()
    {
        isDamaged = true;

        yield return new WaitForSeconds(damageGraceTime);

        isDamaged = false;
    }

    public void CoroutineTookDamage()
    {
        StartCoroutine(TookDamage());
    }
    
    


    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TookDamage());
            eyeMechanics.StartDamagedCoroutine();
            pHealth.health -= damage;
        }
    }
}
