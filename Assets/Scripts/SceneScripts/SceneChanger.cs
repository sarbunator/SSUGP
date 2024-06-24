using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private static SceneChanger instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene("MainGame");
    }

    
    public void LoadMainMenu()
    {
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
