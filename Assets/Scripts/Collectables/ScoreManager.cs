using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text scoreText;
    private int score = 0;
    private List<int> highScores = new List<int>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        highScores.Add(score);
        highScores.Sort((a, b) => b.CompareTo(a)); // J‰rjest‰‰ suurimmasta pienimp‰‰n
        if (highScores.Count > 10)
            highScores.RemoveAt(10); // S‰ilytt‰‰ vain 10 parasta tulosta

        score = 0;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "" + score;
    }

    public List<int> GetHighScores()
    {
        return highScores;
    }

    void Update()
    {
        
    }
}
