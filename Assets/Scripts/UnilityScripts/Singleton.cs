using UnityEngine;

public class Singleton : MonoBehaviour
{

    public static Singleton Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // make sure the Singleton persists between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
