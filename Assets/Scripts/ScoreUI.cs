using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    // Parte visual de la puntuaci�n

    [SerializeField] private TextMeshProUGUI scoreText;
    
    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}
