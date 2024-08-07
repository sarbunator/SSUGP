using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed;
    public GameObject player;
    public float detectionRange;
    public PlayerHealth playerHealth;

    public Animator animator;

    private int targetPoint;
    private bool isChasingPlayer;
    private float closeEnoughDistance = 0.1f; // Threshold to consider the NPC has reached a waypoint

    public SharkProgressiveDifficulty progDifficulty;

    void Start()
    {
        targetPoint = 0;
        isChasingPlayer = false;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        StatCheck();

        if (distanceToPlayer < detectionRange && playerHealth.isDead == false)
        {
            // Start chasing the player
            isChasingPlayer = true;
            animator.SetBool("isAggro", isChasingPlayer);
        }
        else if (distanceToPlayer >= detectionRange && isChasingPlayer || playerHealth.isDead)
        {
            // Stop chasing the player and return to patrolling
            isChasingPlayer = false;
            animator.SetBool("isAggro", isChasingPlayer);
            targetPoint = GetClosestPatrolPointIndex();
        }

        if (isChasingPlayer)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, patrolPoints[targetPoint].position) < closeEnoughDistance)
        {
            targetPoint = (targetPoint + 1) % patrolPoints.Length;
        }

        MoveTowards(patrolPoints[targetPoint].position);
    }

    void ChasePlayer()
    {
        MoveTowards(player.transform.position);
    }

    void MoveTowards(Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void StatCheck()
    {
        if (progDifficulty.sharkSpeed > speed) 
        {
            speed = progDifficulty.sharkSpeed;
        }
        if (progDifficulty.aggroRange > detectionRange)
        {
            detectionRange = progDifficulty.aggroRange;
        }
    }

    int GetClosestPatrolPointIndex()
    {
        int closestIndex = 0;
        float closestDistance = Vector2.Distance(transform.position, patrolPoints[0].position);

        for (int i = 1; i < patrolPoints.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, patrolPoints[i].position);
            if (distance < closestDistance)
            {
                closestIndex = i;
                closestDistance = distance;
            }
        }

        return closestIndex;
    }
}
