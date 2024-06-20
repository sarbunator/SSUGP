using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int inkCount;
    public int maxInkCount = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("GameManager Awake called.");
            Instance = this;
            DontDestroyOnLoad(gameObject); // make sure the GameManager persists between scenes
            inkCount = maxInkCount;
            Debug.Log("GameManager instance created.");
        }
        else
        {
            Debug.LogWarning("Duplicate GameManager instance detected and destroyed.");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("GameManager OnDestroy called.");
    }

    public bool UseInk(int amount)
    {
        if (inkCount >= amount)
        {
            inkCount -= amount;
            return true;
        }
        return false;
    }
    public void RefillInk(int amount)
    {
        inkCount = Mathf.Min(inkCount + amount, maxInkCount); // ensure ink count doesnt go over max
    }
}