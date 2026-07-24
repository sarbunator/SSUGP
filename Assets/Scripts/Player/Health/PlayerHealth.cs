using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// k‰ytin MoreBBlakeyy -youtubekanavan ohjevideota:
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

    private PlayerControls playerControls;

    private void Awake()
    {
        // Luo PlayerControls instanssi
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        // Gameplay on p‰‰ll‰ kun pelaaja on elossa
        playerControls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();
    }

    void Start()
    {
        health = maxHealth;
        isDead = false;

        // Always search for HealthBar on scene load to handle restarts properly
        GameObject healthBarObject = GameObject.Find("Managers/UiManager/Canvas/GameUI/HealthBar");

        if (healthBarObject != null)
        {
            healthBar = healthBarObject.GetComponent<Image>();
            Debug.Log("HealthBar found and connected!");
        }
        else
        {
            // Fallback: search for any Image component with name "HealthBar"
            healthBar = FindAnyObjectByType<Image>();

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

            //Disables ALL gamescripts and colliders that would allow the player to move or interact with the world, also plays death animation and sound
            //source.PlayOneShot(audioDeath);
            //source.Stop();
            if (cameraTargeting != null)
                cameraTargeting.enabled = false;
            if (eyeMechanics != null)
                eyeMechanics.enabled = false;
            if (moveUnderwater != null)
                moveUnderwater.enabled = false;
            if (inkShooting != null)
                inkShooting.enabled = false;
            if (polygonCollider != null)
                polygonCollider.enabled = false;

            // IMPORTANT: Disable player controls to prevent movement and shooting during death sequence
            playerControls.Gameplay.Disable();
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