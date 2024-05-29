using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour
{
    public PlayerInkAmmo pInkAmmo;
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
            pInkAmmo.ink += ink;
            Destroy(gameObject);
        }
    }
}
