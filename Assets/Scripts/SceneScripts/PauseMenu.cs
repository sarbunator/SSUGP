using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject tutorialMenuUI;
    private bool isPaused = false;

    public InkShooting inkShooting;
    public SceneChanger sceneChanger;

    
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

    public void ResumeGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        else
        {
            Debug.LogError("pauseMenuUI is not assigned in PauseMenu script.");
        }

        Time.timeScale = 1f;
        isPaused = false;

        if (inkShooting != null)
        {
            inkShooting.enabled = true;
        }
        else
        {
            Debug.LogError("inkShooting is not assigned in PauseMenu script.");
        }
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
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
        Debug.Log("Returning to pause menu");
    }

    public void QuitGame()
    {
        sceneChanger.QuitGame();
    }
}
