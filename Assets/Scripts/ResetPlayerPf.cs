using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPlayerPf : MonoBehaviour
{
    // Code for reset the high score

    [SerializeField] private GameObject resetHighScorePanel;
    [SerializeField] private Button resetHighScoreButton;
    [SerializeField] private Button noResetHighScoreButton;

    private void Awake()
    {
       // Show the panel to reset the high score or not before the game begins
       ShowResetHighScorePanel();

       resetHighScoreButton.onClick.AddListener(ResetHighScore);
       noResetHighScoreButton.onClick.AddListener(HideResetHighScorePanel);
    }

    private void ShowResetHighScorePanel() // Show the panel and stops the game
    {
        resetHighScorePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void HideResetHighScorePanel() // Hide the panel and the game begins
    {
        resetHighScorePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ResetHighScore() // Delete the previously saved high score, update the visual text and hide the panel
    {
        PlayerPrefs.DeleteAll();
        ScoreUI.Instance.UpdateHighScoreText();
        HideResetHighScorePanel();
    }
}
