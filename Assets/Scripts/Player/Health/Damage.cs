using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public PlayerHealth pHealth;
    public float damage;

    public EyeMechanics eyeMechanics;

    [HideInInspector]
    public bool isDamaged;
    [HideInInspector]
    public bool damageImmunity;
    public float damageGraceTime;

    public SharkProgressiveDifficulty progDifficulty;


    public Animator animator;

    void Start()
    {

    }

    IEnumerator TookDamage()
    {
        damageImmunity = true;
        isDamaged = true;
        animator.SetBool("isDamaged", isDamaged);
        yield return new WaitForSeconds(0.3f);
        isDamaged = false;
        animator.SetBool("isDamaged", isDamaged);

        yield return new WaitForSeconds(damageGraceTime);
        damageImmunity = false;

    }

    public void CoroutineTookDamage()
    {
        StartCoroutine(TookDamage());
    }
    
    void Update()
    {
        if (progDifficulty.sharkDamage > damage)
        {
            damage = progDifficulty.sharkDamage;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TookDamage());
            eyeMechanics.StartDamagedCoroutine();
            FindObjectOfType<AudioManager>().Play(new string[] { "Injury_1", "Injury_2", "Injury_3"});
            pHealth.health -= damage;
        }
    }
}
