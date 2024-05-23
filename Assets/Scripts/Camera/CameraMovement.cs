using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerMouseThresholdPoint;
    
    public Vector3 offset;
    public float speed;
    

    

    void Start()
    {
        
    }
   
    void CameraPosition()
    {
        Vector3 desiredPos = playerMouseThresholdPoint.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, speed * Time.deltaTime);

        Vector3 position = transform.position;
        position.z = -10.0f;
        transform.position = position;
    }

    


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CameraPosition();
    }
}
