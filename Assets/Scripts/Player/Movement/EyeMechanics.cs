using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMechanics : MonoBehaviour
{
    public Transform eye;
    public Transform irisNormal;
    public float irisMovementRadius;

    public Sprite originalEye;
    public Sprite irisAngry;
    public Sprite irisHappy;
    public Sprite irisDead;

    public SpriteRenderer expressionAngry;
    public SpriteRenderer expressionHappy;
    public SpriteRenderer expressionDead;

    public float angryTime;
    public float happyTime;
    public float sadDead;
    public float damagedTime;

    private bool waiting = false;

    public PlayerHealth playerHealth;

    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = irisNormal.GetComponent<SpriteRenderer>();

    }

    public void SpriteChangeIris(int eyeIndex)
    {
        switch (eyeIndex)
        {
            case 0:
                spriteRenderer.sprite = originalEye;
                break;
            case 1:
                spriteRenderer.sprite = irisAngry;
                break;
            case 2:
                spriteRenderer.sprite = irisHappy;
                break;
            case 3:
                spriteRenderer.sprite = irisDead;
                break;
        }
    }
    IEnumerator Damaged()
    {
        SpriteChangeIris(3);
        expressionDead.enabled = true;
        yield return new WaitForSecondsRealtime(damagedTime);
        SpriteChangeIris(0);
        expressionDead.enabled = false;
    }

    public void StartDamagedCoroutine()
    {
        StartCoroutine(Damaged());
    }


    IEnumerator Dead()
    {
        SpriteChangeIris(3);
        expressionDead.enabled = true;
        yield return new WaitForSecondsRealtime(sadDead);
        SpriteChangeIris(0);
        expressionDead.enabled = false;
    }

    public void StartDeadCoroutine()
    {
        StartCoroutine(Dead());
    }

    IEnumerator Angry()
    {
        waiting = true;
        SpriteChangeIris(1);
        expressionAngry.enabled = true;
        yield return new WaitForSecondsRealtime(angryTime);
        waiting = false;
        SpriteChangeIris(0);
        expressionAngry.enabled = false;
        
    }

    IEnumerator Happy()
    {
        expressionHappy.enabled = true;
        SpriteChangeIris(2);
        yield return new WaitForSecondsRealtime(happyTime);
        expressionHappy.enabled = false;
        SpriteChangeIris(0);
    }
    public void StartHappyCoroutine()
    {
        if (waiting)
            return;
        
            StartCoroutine(Happy());
        
        
    }

    public void ExpressionLogistic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Angry());
        }   
    }

    void IrisCursorFollow()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        UpdateIrisPosition(eye, irisNormal, worldMousePosition);
    }


    void Update()
    {
        if (playerHealth.isDead == true)
        {
            StartCoroutine(Dead());
        }
        else
        {
            IrisCursorFollow();
            ExpressionLogistic();
        }

    }

    void UpdateIrisPosition(Transform eye, Transform irisNormal, Vector3 targetPosition)
    {
        Vector2 direction = (targetPosition - eye.position);
        direction.Normalize();
        irisNormal.position = eye.position + (Vector3)direction * irisMovementRadius;
    }

}

