using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }  // Singleton

    private LevelGrid levelGrid;
    private Snake snake;

    private bool isPaused;

    [SerializeField] private Button pauseButton;

    private void Awake() // Singleton
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance Game Manager");
        }

        Instance = this;

        pauseButton.onClick.AddListener(PauseGame);
    }

    private void Start()
    {
        SoundManager.CreateSoundManagerGameObject();

        // Configuration of the snake's head
        GameObject snakeHeadGameObject = new GameObject("Snake Head");
        SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAssets.Instance.snakeHeadSprite;
        snake = snakeHeadGameObject.AddComponent<Snake>();

        // Configuratioin of the LevelGrid
        levelGrid = new LevelGrid(20, 20);
        snake.Setup(levelGrid);
        levelGrid.Setup(snake);

        // Initialize of the score
        Score.InitializeStaticScore();

        // Initialize of the Pause Menu
        isPaused = false;
    }

    private void Update()
    {
        // Activate the Pause Menu iwith Escape Key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        } 
    }

    public void SnakeDied()
    {
        SoundManager.PlaySound(SoundManager.Sound.SnakeDie);
        GameOverUI.Instance.Show(Score.TrySetNewHighScore());
        Timer.Instance.timeLeft = 0; // If we died before the time is up --> timer turn to zero
    }

    // Game's configuration if the Pause Menu is On
    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseUI.Instance.Show();
        isPaused = true;
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
    }

    // Game's configuration if the Pause Menu is Off
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PauseUI.Instance.Hide();
        isPaused = false;
        SoundManager.PlaySound(SoundManager.Sound.ButtonOver);
    }
}
