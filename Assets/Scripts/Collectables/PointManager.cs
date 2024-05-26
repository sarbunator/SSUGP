using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    public int pointCount;
    public Text pointText;
    void Start()
    {
        
    }

    void Update()
    {
        pointText.text = pointCount.ToString();
    }
}
