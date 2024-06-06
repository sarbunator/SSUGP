using UnityEngine;

public class FieldOfVision : MonoBehaviour
{
    public float viewRadius = 5f; // Detection range
    [Range(0, 360)]
    public float viewAngle = 90f; // Field of view angle

    public LayerMask targetMask; // Layer of the target (e.g., player)
    public LayerMask obstacleMask; // Layer of the obstacles

    private void Update()
    {
        FindVisibleTargets();
    }

    void FindVisibleTargets()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        foreach (Collider2D target in targetsInViewRadius)
        {
            Transform targetTransform = target.transform;
            Vector2 directionToTarget = (targetTransform.position - transform.position).normalized;

            if (Vector2.Angle(transform.right, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, targetTransform.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    // The target is within the field of view and not blocked by an obstacle
                    Debug.Log("Target in sight: " + targetTransform.name);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 leftBoundary = DirectionFromAngle(-viewAngle / 2, false);
        Vector3 rightBoundary = DirectionFromAngle(viewAngle / 2, false);

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewRadius);
    }

    public Vector2 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }
}
