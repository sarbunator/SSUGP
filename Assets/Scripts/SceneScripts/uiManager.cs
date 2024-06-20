using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameUI; // gameui, healthbar etc
    [SerializeField] private GameObject gameOverPanel;

    private bool isGameOver = false;

    public PlayerHealth playerHealth; // reference to the PlayerHealth script (liittyy gameoverpanel delay)
    public GameObject tutorialPanel;
    public GameObject pauseGamePanel;
    public GameObject optionsPanel;
    public SceneChanger sceneChanger;

    void Start()
    {
        gameUI.SetActive(true); // GameUI enabloituna
        gameOverPanel.SetActive(false);

        // muut paneelit disabloitu
        tutorialPanel.SetActive(false);
        pauseGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    void Update()
    {
        // onko player dead ja gameover sequence ei oo alkanu viel
        if (playerHealth.isDead && !isGameOver)
        {
            isGameOver = true;
            StartCoroutine(GameOverSequence());
        }

        // tarkista onko muu paneeli active, pitääkö disable GameUI
        bool anyPanelActive = gameOverPanel.activeSelf || tutorialPanel.activeSelf || pauseGamePanel.activeSelf || optionsPanel.activeSelf;
        gameUI.SetActive(!anyPanelActive);
    }

    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(3.0f);
        gameOverPanel.SetActive(true);
    }

    public void RestartGameFromUI()
    {
        Debug.Log("Restarting game from UI...");
        sceneChanger.RestartGame();
        // Reset UI state
        isGameOver = false;
        gameOverPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        pauseGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
        gameUI.SetActive(true); // Ensure gameUI is active
    }

    public void QuitGameFromUI()
    {
        sceneChanger.QuitGame();
    }
}

