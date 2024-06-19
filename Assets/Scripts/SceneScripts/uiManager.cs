using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameUI; // Reference to GameUI GameObject
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text restartText;
    private bool isGameOver = false;

    public PlayerHealth playerHealth; // reference to the PlayerHealth script (liittyy gameoverpanel delay)
    public GameObject tutorialPanel;
    public GameObject pauseGamePanel;
    public GameObject optionsPanel;

    void Start()
    {
        gameUI.SetActive(true); // GameUI enabloituna
        gameOverPanel.SetActive(false);
        //restartText.gameObject.SetActive(false);

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

        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                print("Application Quit");
                Application.Quit();
            }
        }
    }

    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(3.0f); // Delay before showing the Game Over panel
        gameOverPanel.SetActive(true); 
        //restartText.gameObject.SetActive(true);
    }
}
