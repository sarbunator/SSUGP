using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// käytin MoreBBlakeyy -youtubekanavan ohjevideota:
// https://www.youtube.com/watch?v=bRcMVkJS3XQ

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBar;

    public float deathAnimationTime;
    public bool isDead;

    public AudioSource audioDeath;
    public Animator animator;

    public CameraTargeting cameraTargeting;
    public EyeMechanics eyeMechanics;
    public PlayerMoveUnderwater moveUnderwater;
    public InkShooting inkShooting;
    public PolygonCollider2D polygonCollider;
    


    void Start()
    {
        maxHealth = health;
        
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        if (health <= 0)
        {
            StartCoroutine(Death());
           // audioDeath.Play();
            cameraTargeting.enabled = false;
            eyeMechanics.enabled = false;
            moveUnderwater.enabled = false;
            inkShooting.enabled = false;
            polygonCollider.enabled = false;
            // GameOver();
        }
    }

    IEnumerator Death()
    {
        isDead = true;
        animator.SetBool("isDead", isDead);
        yield return new WaitForSecondsRealtime(deathAnimationTime);
        isDead = false;


    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOverScene"); // Make sure "GameOver" is the exact name of your game over scene
    }
}