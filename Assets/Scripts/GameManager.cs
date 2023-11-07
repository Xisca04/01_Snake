using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }  // Singleton

    // Como es una constante el nombre va así: MAYUS_MAYUS_MAYUS
    public const int POINTS = 100; // Cantidad de puntos que ganamos al comer la fruta
    private int score; // Puntuación del jugador

    private LevelGrid levelGrid;
    private Snake snake;

    private ScoreUI scoreUIScript;

    private bool isPaused;

    private void Awake() // Singleton
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance Game Manager");
        }

        Instance = this;
    }

    private void Start()
    {
        SoundManager.CreateSoundManagerGameObject();

        // Configuración de la cabeza de serpiente
        GameObject snakeHeadGameObject = new GameObject("Snake Head");
        SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAssets.Instance.snakeHeadSprite;
        snake = snakeHeadGameObject.AddComponent<Snake>();

        // Configurar el LevelGrid
        levelGrid = new LevelGrid(20, 20);
        snake.Setup(levelGrid);
        levelGrid.Setup(snake);

        // Inicializo Score
        scoreUIScript = GetComponentInChildren<ScoreUI>(); // Referencia entre ScoreUI script y GameManager script para poder usar sus variables
        score = 0;
        AddScore(0);

        // Inicializo PauseMenu
        isPaused = false;
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            Loader.Load(Loader.Scene.Game);
        }
        */

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

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreUIScript.UpdateScoreText(score);
    }

    public void SnakeDied()
    {
        GameOverUI.Instance.Show();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseUI.Instance.Show();
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PauseUI.Instance.Hide();
        isPaused = false;
    }
}
