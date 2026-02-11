using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject tutorialMenuUI;
    private bool isPaused = false;

    public InkShooting inkShooting;
    public SceneChanger sceneChanger;

    private PlayerControls playerControls;

    void Update()
    {
       //OldEscPauseMenu();

    }

    private void Awake()
    {
        playerControls = new PlayerControls();

        // Rekisteröi Pause action PlayerUI action mapista
        playerControls.PlayerUI.Pause.performed += OnPause;

        // Aluksi PlayerUI on disabloitu, Gameplay on päällä
        // (PlayerMoveUnderwater ja InkShooting hoitavat Gameplay enablen)
    }

    private void OnEnable()
    {
        // Aloita PlayerUI kuuntelemaan pause nappia
        playerControls.PlayerUI.Enable();
    }

    private void OnDisable()
    {
        playerControls.PlayerUI.Disable();
        playerControls.Gameplay.Disable();
    }

    private void OnDestroy()
    {
        playerControls.PlayerUI.Pause.performed -= OnPause;
    }

    #region Input Callbacks

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Navigoi menuissa
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

    #endregion

    public void ResumeGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        Time.timeScale = 1f;
        isPaused = false;

        // Vaihda PlayerUI -> Gameplay
        playerControls.PlayerUI.Disable();
        playerControls.Gameplay.Enable();

        // Aktivoi peliskriptit
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

        // Vaihda Gameplay -> PlayerUI (vain UI kontrollit toimivat)
        playerControls.Gameplay.Disable();
        playerControls.PlayerUI.Enable();

        // Deaktivoi peliskriptit
        inkShooting = FindAnyObjectByType<InkShooting>();
        if (inkShooting != null)
        {
            inkShooting.enabled = false;
        }
        
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
        if (optionsMenuUI != null)
        {
            optionsMenuUI.SetActive(false);
        }
        if (tutorialMenuUI != null)
        {
            tutorialMenuUI.SetActive(false);
        }
        pauseMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        sceneChanger.QuitGame();
    }


    void OldEscPauseMenu()
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
}
