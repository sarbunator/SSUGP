using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeInkExplosion : MonoBehaviour
{
    public int waitTime;
    public float fadingTime;

    private Material material;

    public float effectTime;
    public Collider2D inkCollider2D;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        StartCoroutine(InkCloudFading());
        StartCoroutine(CollisionRemover());
    }

    IEnumerator CollisionRemover()
    {
        yield return new WaitForSeconds(effectTime);
        inkCollider2D.enabled = false;
    }

    IEnumerator InkCloudFading()
    {
        float startAlpha = material.color.a;
        float time = 0;
        yield return new WaitForSeconds(waitTime);


        while (time < fadingTime)
        {
            time += Time.deltaTime;
            float blend = Mathf.Clamp01(time / fadingTime);
            Color color = material.color;
            color.a = Mathf.Lerp(startAlpha, 0.0f, blend);
            material.color = color;
            yield return null;

        }

        Destroy(gameObject);

    }


    void Update()
    {
        
    }
}
