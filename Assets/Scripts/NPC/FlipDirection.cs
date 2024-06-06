using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class FlipDirection : MonoBehaviour
{
    private Vector3 previousPosition;
    private bool facingLeft = true;
    public float movementThreshold;

    void Start()
    {
        // Saves the starting position
        previousPosition = transform.position;
    }

    void Update()
    {
        CheckAndFlipDirection();

    }

    void CheckAndFlipDirection()
    {
        
        Vector3 movement = transform.position - previousPosition; // Calculates current position and previous position difference.

        if (Mathf.Abs(movement.x) > movementThreshold)
        {
            if (movement.x < 0 && !facingLeft) // If movement is greater than zero->Flip
            {
                Flip();
            }
            else if (movement.x > 0 && facingLeft)
            {
                Flip();
            }
        }

        

        previousPosition = transform.position; // Update position for the next frame
    }

    void Flip()
    {
        // flip by changing the localscale between + / -
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}
