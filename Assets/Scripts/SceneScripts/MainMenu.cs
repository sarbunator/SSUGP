using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("PauseMenu Awake called.");
    }

    void Start()
    {
        Debug.Log("PauseMenu Start called.");
    }

    void OnDestroy()
    {
        Debug.Log("PauseMenu OnDestroy called.");
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
   
    public void QuitGame()
    {

        Application.Quit();
    }
}
