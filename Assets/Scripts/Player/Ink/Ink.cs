using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInk : MonoBehaviour
{
    public PlayerInk pInk;
    public float ink;
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pInk.ink += ink;
            Destroy(gameObject);
        }
    }
}