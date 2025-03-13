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
    [HideInInspector]
    public bool isDead;


    //public AudioSource source;
    //public AudioClip audioDeath;
    public Animator animator;

    public CameraTargeting cameraTargeting;
    public EyeMechanics eyeMechanics;
    public PlayerMoveUnderwater moveUnderwater;
    public InkShooting inkShooting;
    public PolygonCollider2D polygonCollider;

    //private void Awake()
    //{
    //    if (healthBar == null)
    //    {
    //        GameObject healthBarObject = GameObject.Find("Canvas/GameUI/HealthBar");

    //        if (healthBarObject != null)
    //        {
    //            healthBar = healthBarObject.GetComponent<Image>();
    //        }
    //        else
    //        {
    //            Debug.LogError("HealthBar not found! Check the hierarchy path.");
    //        }
    //    }
    //}

    void Start()
    {
        maxHealth = health;
        if (healthBar != null)
        {
            GameObject healthBarObject = GameObject.Find("/UiManager/Canvas/GameUI/HealthBar");

            if (healthBarObject != null)
            {
                healthBar = healthBarObject.GetComponent<Image>();
            }
            else
            {
                // Jos nimeen perustuva haku ei toimi, etsitään Image-komponentti suoraan
                healthBar = FindObjectOfType<Image>();

                // Tarkistetaan, että löytynyt Image on oikea HealthBar
                if (healthBar != null && healthBar.name == "HealthBar")
                {
                    Debug.Log("HealthBar found dynamically!");
                }
                else
                {
                    Debug.LogError("HealthBar not found! Check the hierarchy path.");
                    healthBar = null;
                }
            }
        }

    }

    void Update()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        }

        if (health <= 0 && !isDead)
        {
            eyeMechanics.StartDeadCoroutine();
            StartCoroutine(Death());
            //source.PlayOneShot(audioDeath);
            //source.Stop();
            cameraTargeting.enabled = false;
            eyeMechanics.enabled = false;
            moveUnderwater.enabled = false;
            inkShooting.enabled = false;
            polygonCollider.enabled = false;
            
        }
    }

    IEnumerator Death()
    {
        isDead = true;  
        animator.SetBool("isDead", isDead);
        yield return new WaitForSecondsRealtime(deathAnimationTime);
        GameManager.Instance.GameOver();
    }

}