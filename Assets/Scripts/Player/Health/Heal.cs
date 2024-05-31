using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public float healAmount; // healin m‰‰r‰

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.health += healAmount;
                playerHealth.health = Mathf.Clamp(playerHealth.health, 0, playerHealth.maxHealth);
                Destroy(gameObject);
            }
        }
    }
}   