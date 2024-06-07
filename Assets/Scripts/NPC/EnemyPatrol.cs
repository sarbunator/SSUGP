using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
    public GameObject player;
    public float distanceBetween;

    private float distance;
 
    void Start()
    {
        targetPoint = 0;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed = Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
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
