using UnityEngine;
using UnityEngine.UI;

public class InkMeterController : MonoBehaviour
{
    public Image inkMeterImage;

    private void Awake()
    {
        if (inkMeterImage == null)
        {
            inkMeterImage = GetComponent<Image>();
        }
    }

    private void Update()
    {
        // Update the ink meter based on the current ink count
        inkMeterImage.fillAmount = GameManager.Instance.inkCount / 10f; // Assuming 10 is the max ink count
    }
}