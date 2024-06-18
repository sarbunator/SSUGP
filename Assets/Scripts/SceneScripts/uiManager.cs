using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text restartText;
    private bool isGameOver = false;

    public PlayerHealth playerHealth; // reference to the PlayerHealth script (liittyy gameoverpanel delay)

    void Start()
    {
        gameOverPanel.SetActive(false);
        restartText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerHealth.isDead && !isGameOver)
        {
            isGameOver = true;
            StartCoroutine(GameOverSequence());
        }

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
        restartText.gameObject.SetActive(true);
    }
}
