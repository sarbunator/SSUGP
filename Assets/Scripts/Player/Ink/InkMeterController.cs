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
        // update the ink meter based on the current ink count
        inkMeterImage.fillAmount = GameManager.Instance.inkCount / 10f; // assuming 10 is the max ink count
    }
}