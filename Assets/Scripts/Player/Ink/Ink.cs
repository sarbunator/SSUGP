using UnityEngine;

public class Ink : MonoBehaviour
{
    public int inkAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.RefillInk(inkAmount); //game manager hoitaaa
            Destroy(gameObject);
        }
    }
}