using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    public int pointCount;
    public Text pointText;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        // Try to find the pointText if it's not assigned in the inspector
        if (pointText == null)
        {
            GameObject pointTextObject = GameObject.Find("PointText");
            if (pointTextObject != null)
            {
                pointText = pointTextObject.GetComponent<Text>();
            }
            else
            {
                Debug.LogError("PointText object not found in the scene.");
            }
        }
    }

  

    void Update()
    {
        if (pointText != null)
        {
            pointText.text = pointCount.ToString();
        }
        else
        {
            Debug.LogError("pointText is not assigned and could not be found in the scene.");
        }
        if (pointText == null)   // copied from the above
        {
            GameObject pointTextObject = GameObject.Find("PointText");
            if (pointTextObject != null)
            {
                pointText = pointTextObject.GetComponent<Text>();
            }
        }
    }
}
