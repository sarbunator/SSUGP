using UnityEngine;

public class ShowOnMobile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        gameObject.SetActive(Application.isMobilePlatform);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
