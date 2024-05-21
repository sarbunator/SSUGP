using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMoveUnderwater : MonoBehaviour
{

    public Rigidbody2D rb;

    [SerializeField] public float movementForce;

    private Vector2 moveDirection;

    // Dash kokeilu 
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    [SerializeField] private TrailRenderer tr;

    public PointManager pm;
    void ProcessInputs()
    {
        if (isDashing)
        {
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

    }

    //          ******* Dashing script guide *******
    // https://www.youtube.com/watch?v=2kFGmuPHiA0&ab_channel=bendux
    //          ************************************
    void Move()
    {

        rb.AddForce(new Vector2(moveDirection.x * movementForce, moveDirection.y * movementForce));
        // rb.AddForce(moveDirection * movementForce); is the same as above -_- damn ChatGPT
        
    } 

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.AddForce(moveDirection * dashingPower * movementForce);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }


    void Update()
    {

        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pearl_1"))
        {
            Destroy(other.gameObject);
            pm.pointCount++;
        }
    }
        
}
