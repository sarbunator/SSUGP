using UnityEngine;

public class FlipDirection : MonoBehaviour
{
    [Header("Flip Settings")]
    [Tooltip("Minimum movement distance required to trigger flip")]
    public float movementThreshold = 0.01f;

    [Tooltip("Time in seconds before allowing another flip (prevents rapid flipping)")]
    public float flipCooldown = 0.1f;

    [Header("Optional - Game State Check")]
    [Tooltip("Optional: Reference to PlayerHealth to stop flipping when player is dead")]
    public PlayerHealth playerHealth;

    private Vector3 previousPosition;
    private bool facingLeft = true;
    private float lastFlipTime;

    void Start()
    {
        previousPosition = transform.position;
        lastFlipTime = -flipCooldown;
    }

    void Update()
    {
        if (ShouldCheckForFlip())
        {
            CheckAndFlipDirection();
        }
    }

    bool ShouldCheckForFlip()
    {
        if (playerHealth != null && playerHealth.isDead)
        {
            return false;
        }

        return true;
    }

    void CheckAndFlipDirection()
    {
        Vector3 movement = transform.position - previousPosition;

        if (Mathf.Abs(movement.x) > movementThreshold)
        {
            if (Time.time - lastFlipTime >= flipCooldown)
            {
                if (movement.x < 0 && !facingLeft)
                {
                    Flip();
                }
                else if (movement.x > 0 && facingLeft)
                {
                    Flip();
                }
            }
        }

        previousPosition = transform.position;
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        lastFlipTime = Time.time;
    }
}
