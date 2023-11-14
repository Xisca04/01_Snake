using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score 
{
    public const string HIGH_SCORE = "highScore"; // Clave en PlayerPrefs
    
    // Como es una constante el nombre va as�: MAYUS_MAYUS_MAYUS
    public const int POINTS = 100; // Cantidad de puntos que ganamos al comer la fruta
    private static int score; // Puntuaci�n del jugador

    private static ScoreUI scoreUIScript;

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE, 0);
    }

    public static bool TrySetNewHighScore(int score) // socore = puntuaion actual
    {
        int highScore = GetHighScore();

        if (score > highScore)
        {
           // Modificamos el High Score
            PlayerPrefs.SetInt(HIGH_SCORE, score);
            PlayerPrefs.Save();
            return true;
        }

        return false;
    }

    public static void InitializeStaticScore()
    {
        score = 0;
        AddScore(0);
    }

    public static int GetScore()
    {
        return score;
    }

    public static void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        ScoreUI.Instance.UpdateScoreText(score);
    }
}
