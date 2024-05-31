using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInkAmmo : MonoBehaviour
{
    public float ink;
    public float maxInk;
    public Image inkMeter;

    void Start()
    {
        maxInk = ink;
    }

    void Update()
    {
        inkMeter.fillAmount = Mathf.Clamp(ink / maxInk, 0, 1);
    }
}
