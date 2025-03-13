using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject gameOverPanel;
    public TextMeshProUGUI highScoreText;

    public int inkCount;
    public int maxInkCount = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // make sure the GameManager persists between scenes
            inkCount = maxInkCount;
        }
        else
        {
            Debug.LogWarning("Duplicate GameManager instance detected and destroyed.");
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        inkCount = maxInkCount;
    }


    public bool UseInk(int amount)
    {
        if (inkCount >= amount)
        {
            inkCount -= amount;
            return true;
        }
        return false;
    }
    public void RefillInk(int amount)
    {
        inkCount = Mathf.Min(inkCount + amount, maxInkCount); // ensure ink count doesnt go over max
    }

    public void ResetInkCount()
    {
        inkCount = maxInkCount;
    }

    public void GameOver()
    {
        ScoreManager.Instance.ResetScore();
        ShowHighScores();
        gameOverPanel.SetActive(true);
        ResetInkCount();
    }

    private void ShowHighScores()
    {
        highScoreText.text = "Top 10 Scores:\n";
        var scores = ScoreManager.Instance.GetHighScores();
        for (int i = 0; i < scores.Count; i++)
        {
            highScoreText.text += $"{i + 1}. {scores[i]}\n";
        }
    }
}