using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPlayerPf : MonoBehaviour
{
    // Reset all the player prefs (high score)
    /*
     * if panel high score active --> time scale a 0
     * else if panel desactive --> time scale a 1
     *
     */

    [SerializeField] private GameObject resetHighScorePanel;
    [SerializeField] private Button resetHighScoreButton;
    [SerializeField] private Button noResetHighScoreButton;

    private void Awake()
    {
       ShowResetHighScorePanel();
       resetHighScoreButton.onClick.AddListener(ResetHighScore);
       noResetHighScoreButton.onClick.AddListener(HideResetHighScorePanel);
    }

    private void ShowResetHighScorePanel()
    {
        resetHighScorePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void HideResetHighScorePanel()
    {
        resetHighScorePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteAll();
        ScoreUI.Instance.UpdateHighScoreText();
        HideResetHighScorePanel();
    }
}
