using UnityEngine;

public class PlayerCollectibles : MonoBehaviour
{
    public PointManager pm;
    public EyeMechanics eyeMechanics;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pearl_White"))
        {
            Destroy(other.gameObject);
            pm.pointCount += 1;
            eyeMechanics.StartHappyCoroutine();
        }
        else if (other.gameObject.CompareTag("Pearl_Purple"))
        {
            Destroy(other.gameObject);
            pm.pointCount += 5;
            eyeMechanics.StartHappyCoroutine();
        }
        else if (other.gameObject.CompareTag("Pearl_Golden"))
        {
            Destroy(other.gameObject);
            pm.pointCount += 25;
            eyeMechanics.StartHappyCoroutine();
        }
        else if (other.gameObject.CompareTag("Healing_Potion"))
        {
            eyeMechanics.StartHappyCoroutine();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("InkBottle"))
        {
            eyeMechanics.StartHappyCoroutine();
            Destroy(other.gameObject);
        }
    }
}