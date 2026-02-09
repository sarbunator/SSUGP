using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerMouseThresholdPoint;
    
    public Vector3 offset;
    public float speed;
    

    

    void Start()
    {
        if (playerMouseThresholdPoint != null)
        {
            Vector3 initialPos = playerMouseThresholdPoint.position + offset;
            initialPos.z = -15.0f;
            transform.position = initialPos;
        }
    }
   
    void CameraPosition()
    {
        if (playerMouseThresholdPoint == null)
        {
            Debug.LogWarning("CameraMovement: playerMouseThresholdPoint is not assigned!");
            return;
        }

        Vector3 desiredPos = playerMouseThresholdPoint.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, speed * Time.deltaTime);

        Vector3 position = transform.position;
        position.z = -15.0f;
        transform.position = position;
    }

    


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //CameraPosition();
    }

    private void LateUpdate()
    {
        CameraPosition();

    }
}
