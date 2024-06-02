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

    public SpriteRenderer expressionAngry;
    public SpriteRenderer expressionHappy;

    public float angryTime;
    public float happyTime;

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
                default: // this can be removed if it works properly
                Debug.LogError("Something went wrong with the sprite");
                break;
        }
    }



    IEnumerator Angry()
    {
        SpriteChangeIris(1);
        expressionAngry.enabled = true;
        yield return new WaitForSecondsRealtime(angryTime);

        SpriteChangeIris(0);
        expressionAngry.enabled = false;
        
    }
    public void StartHappyCoroutine()
    {
        StartCoroutine(Happy());
    }

    IEnumerator Happy()
    {
        expressionHappy.enabled = true;
        SpriteChangeIris(2);
        yield return new WaitForSecondsRealtime(happyTime);
        expressionHappy.enabled = false;
        SpriteChangeIris(0);
    }


    //  SetSprite(0); // change to original sprite
    //  SetSprite(1); // change to angry sprite
    //  SetSprite(2); // change to happy sprite

    
    

    public void ExpressionLogistic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Angry());
        }
        
    }




    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        UpdateIrisPosition(eye, irisNormal, worldMousePosition);

        ExpressionLogistic();
    }

    void UpdateIrisPosition(Transform eye, Transform irisNormal, Vector3 targetPosition)
    {
        Vector2 direction = (targetPosition - eye.position);
        direction.Normalize();
        irisNormal.position = eye.position + (Vector3)direction * irisMovementRadius;
    }

}

