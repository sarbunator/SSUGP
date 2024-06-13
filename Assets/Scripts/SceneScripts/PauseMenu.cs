using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadSceneAsync(0);
    }


    public void QuitGame()
    {

    }

    public void Options()
    {

    }


}
