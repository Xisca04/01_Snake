using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPf : MonoBehaviour
{
    // Reset all the player prefs (high score)

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteAll();
        ScoreUI.Instance.UpdateHighScoreText();
    }
}
