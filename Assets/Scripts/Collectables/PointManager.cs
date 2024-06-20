using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    public int pointCount;
    public Text pointText;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Ensure the entire GameObject persists across scenes
        Debug.Log("PointManager Awake called.");
    }

    void Start()
    {
        Debug.Log("PointManager Start called.");

        // Try to find the pointText if it's not assigned in the inspector
        if (pointText == null)
        {
            GameObject pointTextObject = GameObject.Find("PointText");
            if (pointTextObject != null)
            {
                pointText = pointTextObject.GetComponent<Text>();
                Debug.Log("PointText object found and assigned.");
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
    }
    }

