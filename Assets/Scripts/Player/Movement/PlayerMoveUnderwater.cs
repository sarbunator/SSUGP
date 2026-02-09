using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMoveUnderwater : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator animator;

    public AudioSource audioSource;
    public AudioClip swim;

    [SerializeField] public float movementForce;

    private Vector2 moveDirection;
    public bool isFacingLeft = true;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    private bool wasMoving = false;

    // Underwater movement variables
    [SerializeField] float acceleration = 25f;
    [SerializeField] float maxSpeed = 6f;
    [SerializeField] float waterDrag = 3f;

    [SerializeField] private TrailRenderer tr;

    public EyeMechanics eyeMechanics;

    public PointManager pm;

    private PlayerControls playerControls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = FindObjectOfType<PointManager>();

        // Luo Input System
        playerControls = new PlayerControls();

        // Rekisteröi callbackit
        playerControls.Gameplay.Move.started += OnMove;
        playerControls.Gameplay.Move.performed += OnMove;
        playerControls.Gameplay.Move.canceled += OnMove;
        playerControls.Gameplay.Dash.performed += OnDash;

    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();
    }

    private void OnDestroy()
    {
        playerControls.Gameplay.Move.started -= OnMove;
        playerControls.Gameplay.Move.performed -= OnMove;
        playerControls.Gameplay.Move.canceled -= OnMove;
        playerControls.Gameplay.Dash.performed -= OnDash;
    }

    void Start()
    {

    }

    void Update()
    {
        SwimmingSound();
        //ProcessInputs();
    }

    private void FixedUpdate()
    {
        //Move();
        UnderwaterMove();
        //CheckAndFlipDirection();
    }

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

        bool isMoving = moveDirection != Vector2.zero;
        animator.SetBool("isMoving", isMoving);

        if (!isMoving)
        {
            audioSource.clip = swim;
            audioSource.Play();
        }

    }

    //          ******* Dashing script guide *******
    // https://www.youtube.com/watch?v=2kFGmuPHiA0&ab_channel=bendux
    //          ************************************
    void Move()
    {

        rb.AddForce(moveDirection * movementForce * Time.fixedDeltaTime, ForceMode2D.Force);
    
        //rb.AddForce(new Vector2(moveDirection.x * movementForce, moveDirection.y * movementForce));
        // rb.AddForce(moveDirection * movementForce); is the same as above -_- damn ChatGPT

    }

    void UnderwaterMove()
    {
        if (isDashing) return;


        if (moveDirection.sqrMagnitude > 0.01f)
        {
            // VÄLITÖN reagointi inputtiin - aseta target velocity suoraan
            Vector2 targetVelocity = moveDirection * maxSpeed;
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Nopea hidastus kun ei inputtia
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, waterDrag * Time.fixedDeltaTime);
        }

        CheckAndFlipDirection();
    }



    #region Movement Callbacks

    public void OnMove(InputAction.CallbackContext context)
    {
        // Lue input ja normalisoi AINA (sama kuin vanha ProcessInputs)
        moveDirection = context.ReadValue<Vector2>();

        animator.SetBool("isMoving", moveDirection.sqrMagnitude > 0.01f);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    #endregion

    public void Flip()
            {
                isFacingLeft = !isFacingLeft;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

    // Method that checks and changes the direction of the player if needed
    void CheckAndFlipDirection()
    {
        if ((moveDirection.x < 0 && !isFacingLeft) || (moveDirection.x > 0 && isFacingLeft))
        {
            Flip();
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.AddForce(moveDirection.normalized * dashingPower, ForceMode2D.Impulse);
        tr.emitting = true;
        animator.SetBool("isDashing", isDashing);
        FindObjectOfType<AudioManager>().Play(new string[] { "Dash_1", "Dash_2", "Dash_3", "Dash_4" });
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("isDashing", isDashing);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

        
    }

    void SwimmingSound()
    {
        // Äänitehoste logiikka (sama kuin vanhassa ProcessInputs)
        bool isMoving = moveDirection.sqrMagnitude > 0.01f;

        if (wasMoving && !isMoving)
        {
            audioSource.clip = swim;
            audioSource.Play();
        }

        wasMoving = isMoving;
    }
        
}
