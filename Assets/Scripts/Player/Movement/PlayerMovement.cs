using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;

    public Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2 (moveX, moveY).normalized;

    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y  * moveSpeed);
    }



    void FixedUpdate()
    {
        Move();
        // Physics Calculation
    }

    void Update()
    {
        ProcessInputs();
    }

}
