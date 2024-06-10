using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float duration;
    public float magnitude;
 
    public IEnumerator Shake()
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Apply shake relative to the current position
            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

      
            yield return null;
        }

        // Reset to the original position
        transform.localPosition = originalPos;
        Debug.Log("Shake complete.");
    }
}
