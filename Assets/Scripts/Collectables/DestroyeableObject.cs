using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyableObject : MonoBehaviour
{
    public delegate void Destroyed();
    public event Destroyed OnDestroyed;

    // Call this method to destroy the object
    public void DestroyObject()
    {
        OnDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
    
