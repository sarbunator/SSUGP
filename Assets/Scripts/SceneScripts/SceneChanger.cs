using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private static SceneChanger instance;

    void Awake()
    {
        Debug.Log("SceneChanger Awake called.");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("SceneChanger instance created.");
        }
        else
        {
            Debug.LogWarning("Duplicate SceneChanger instance detected and destroyed.");
            Destroy(gameObject);
        }
        
    }

    void Start()
    {
        Debug.Log("PauseMenu Start called.");
    }

    void OnDestroy()
    {
        Debug.Log("PauseMenu OnDestroy called.");
    }

    public void LoadLevelScene()
    {
        Debug.Log("Loading MainGame scene.");
        SceneManager.LoadScene("MainGame");
    }

    
    public void LoadMainMenu()
    {
        Debug.Log("Loading MainMenu scene.");
        SceneManager.LoadScene("MainMenu");
    }

    
    public void RestartGame()
    {
        Debug.Log("Restarting game...");
        Time.timeScale = 1f; // game not paused
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    
    public void QuitGame()
    {
        Application.Quit();
    }
}
