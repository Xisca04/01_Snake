using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score 
{
    public const string HIGH_SCORE = "highScore"; // Clave en PlayerPrefs
                                                  
    // The constants' name are like this: MAYUS_MAYUS_MAYUS
    public const int POINTS = 100; // Points that we will win if the snake eats the fruit

    public static event EventHandler OnHighScoreChange; // Event

    private static int score; // Player' score
    private static ScoreUI scoreUIScript;

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE, 0);
    }

    public static bool TrySetNewHighScore() // score = current puntuation
    {
        int highScore = GetHighScore();

        if (score > highScore)
        {
           // Modificamos el High Score
            PlayerPrefs.SetInt(HIGH_SCORE, score);
            PlayerPrefs.Save();

            if(OnHighScoreChange != null)
            {
                OnHighScoreChange(null, EventArgs.Empty);
            }
            
            return true;
        }

        return false;
    }

    public static void InitializeStaticScore()
    {
        OnHighScoreChange = null;
        score = 0;
        AddScore(0);
        ScoreUI.Instance.UpdateHighScoreText();
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
