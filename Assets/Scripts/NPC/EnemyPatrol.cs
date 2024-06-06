using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
 
    void Start()
    {
        targetPoint = 0;
    }

    void Update()
    {
        if (transform.position == patrolPoints[targetPoint].position) 
        {
            increaseTargetInt();
        }

        transform.position = Vector2.MoveTowards(transform.position,
            patrolPoints[targetPoint].position, speed * Time.deltaTime);

        
    }


    void increaseTargetInt()
    { 
        targetPoint++;
        if(targetPoint >= patrolPoints.Length) 
        {
            targetPoint = 0;
        }
    }
}
