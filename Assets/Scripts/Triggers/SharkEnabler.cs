using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkEnabler : MonoBehaviour
{
    public GameObject enableObject;
    public GameObject thisTrigger;

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
            enableObject.SetActive(true);
            Destroy(thisTrigger);

        }

    }
}
