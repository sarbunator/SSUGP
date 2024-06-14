using UnityEngine;

public class PlayerCollectibles : MonoBehaviour
{
    public PointManager pm;
    public EyeMechanics eyeMechanics;

  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pearl_White"))
        {
            FindObjectOfType<AudioManager>().Play("PearlWhite");
            Destroy(other.gameObject);
            pm.pointCount += 1;
            eyeMechanics.StartHappyCoroutine();
        }
        else if (other.gameObject.CompareTag("Pearl_Purple"))
        {
            FindObjectOfType<AudioManager>().Play("PearlPurple");
            Destroy(other.gameObject);
            pm.pointCount += 5;
            eyeMechanics.StartHappyCoroutine();
        }
        else if (other.gameObject.CompareTag("Pearl_Golden"))
        {
            FindObjectOfType<AudioManager>().Play("PearlGold");
            Destroy(other.gameObject);
            pm.pointCount += 25;
            eyeMechanics.StartHappyCoroutine();
        }
        else if (other.gameObject.CompareTag("Healing_Potion"))
        {
            FindObjectOfType<AudioManager>().Play("Bottle");
            eyeMechanics.StartHappyCoroutine();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("InkBottle"))
        {
            FindObjectOfType<AudioManager>().Play("Bottle");
            eyeMechanics.StartHappyCoroutine();
            Destroy(other.gameObject);
        }
    }
}