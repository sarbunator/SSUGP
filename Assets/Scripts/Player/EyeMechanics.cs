using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMechanics : MonoBehaviour
{
    public Transform eye;
    public Transform irisNormal;
    public float irisMovementRadius;


    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }



   
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        UpdateIrisPosition(eye, irisNormal, worldMousePosition);

    }

    void UpdateIrisPosition(Transform eye, Transform irisNormal, Vector3 targetPosition)
    {
        Vector2 direction = (targetPosition - eye.position);
        direction.Normalize();
        irisNormal.position = eye.position + (Vector3)direction * irisMovementRadius;
    }
}
