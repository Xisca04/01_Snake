using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }  // Singleton

    [SerializeField] private Button restartButton;
    [SerializeField] private Button timerLevelRestartButton;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private UnityEngine.SceneManagement.Scene actualScene;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance Game Over UI");
        }

        Instance = this;

        Hide();

        // Get the current scene
        actualScene = SceneManager.GetActiveScene();

        // If the player is in Game Scene the restart button will load the game scene (original level)
        if(actualScene.name == "Game")
        {
            restartButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.Game); });
        }
        else if(actualScene.name == "TimerLevel") // If the player is in the Timer level the restart button will load the timer level
        {
            timerLevelRestartButton.onClick.AddListener(() => Loader.Load(Loader.Scene.TimerLevel));
        }
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

        // Other way to code the previous line(61)
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
