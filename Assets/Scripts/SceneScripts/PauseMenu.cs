using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject tutorialMenuUI;
    private bool isPaused = false;

    public InkShooting inkShooting;
    public SceneChanger sceneChanger;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenuUI != null && optionsMenuUI.activeSelf)
            {
                BackToPauseMenu();
            }
            else if (tutorialMenuUI != null && tutorialMenuUI.activeSelf)
            {
                BackToPauseMenu();
            }
            else if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void Awake()
    {
        
    }
    public void ResumeGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        Time.timeScale = 1f;
        isPaused = false;

        if (inkShooting != null)
        {
            inkShooting.enabled = true;
        }
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        inkShooting = FindObjectOfType<InkShooting>();
        inkShooting.enabled = false;
    }

    public void OpenOptionsMenu()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void OpenTutorialMenu()
    {
        pauseMenuUI.SetActive(false);
        tutorialMenuUI.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        optionsMenuUI.SetActive(false);
        tutorialMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        sceneChanger.QuitGame();
    }
}
