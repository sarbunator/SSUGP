using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    
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
        Time.timeScale = 1f; // game not paused
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    
    public void QuitGame()
    {
        Application.Quit();
    }
}
