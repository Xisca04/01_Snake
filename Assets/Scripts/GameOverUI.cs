using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }  // Singleton

    [SerializeField] private Button restartButton;
    [SerializeField] private Button timerLevelRestartButton;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance Game Over UI");
        }

        Instance = this;

        restartButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.Game);});
        timerLevelRestartButton.onClick.AddListener(() => Loader.Load(Loader.Scene.TimerLevel));

        Hide();
    }

    public void Show(bool hasNewHighScore)
    {
        UpdateScoreAndHighScore(hasNewHighScore);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateScoreAndHighScore(bool hasNewHighScore)
    {
        scoreText.text = Score.GetScore().ToString();
        highScoreText.text = Score.GetHighScore().ToString();
        messageText.text = hasNewHighScore ? "CONGRATULATIONS" : "DON'T WORRY, NEXT TIME";

        // if (hasNewHighScore)
        // {
        //     messsageText.text = "CONGRATULATIONS";
        // }
        // else
        // {
        //     messsageText.text = "DON'T WORRY, NEXT TIME";
        // }
    }
}
