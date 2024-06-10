using UnityEngine;

public class Player : MonoBehaviour
{
    private CameraShake cameraShake;

    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        if (cameraShake == null)
        {
            Debug.LogError("CameraShake script not found on the main camera.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            
            if (cameraShake != null)
            {
                StartCoroutine(cameraShake.Shake()); // Example duration and magnitude
            }
        }
    }
}
