using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    // Parte visual de la puntuación
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
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();

        int highScore = Score.GetHighScore();
        highScoreText.text = highScore.ToString();
    }

}
