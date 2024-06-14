using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SharkEnabler : MonoBehaviour
{
    public GameObject enableObject;
    public GameObject thisTrigger;
    [HideInInspector]
    public int collideAmount;
    public int triggerAfterCollisionAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collideAmount += 1;
            if (collideAmount == triggerAfterCollisionAmount)
            {
                enableObject.SetActive(true);
                Destroy(thisTrigger);

            }
        }
        

    }
}
