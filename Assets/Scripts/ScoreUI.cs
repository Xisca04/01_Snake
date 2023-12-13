using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    // Visual part of the score
    public static ScoreUI Instance { get; private set; }  // Singleton

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance Score UI");
        }

        Instance = this;

        // We subscribe a method to the event that we've created

        Score.OnHighScoreChange += Score_OnHighScoreChange;
    }

    private void OnDisable()  // Unsuscribe a method to the event
    {
        Score.OnHighScoreChange += Score_OnHighScoreChange;
    }

    private void Score_OnHighScoreChange(object sender, EventArgs e)
    {
        // When we call the event the high score will be updated
        UpdateHighScoreText();
    }

    public void UpdateHighScoreText()
    {
        int highScore = Score.GetHighScore();
        highScoreText.text = highScore.ToString();
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

}
