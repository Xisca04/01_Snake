using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button quitInstructionsPanelButton;

    [SerializeField] private Button originalLevelButton;
    [SerializeField] private Button timerLevelButton;

    [SerializeField] private Button resetHighScoreButton;
    [SerializeField] private Button noResetHighScoreButton;

    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject chooseLevelPanel;
    [SerializeField] private GameObject resetHighScorePanel;

    private void Awake()
    {
        playButton.onClick.AddListener(ShowResetHighScorePanel);

        resetHighScoreButton.onClick.AddListener(ButtonResetHighScore);
        noResetHighScoreButton.onClick.AddListener(ButtonNoResetHighScore);

        originalLevelButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.Game); SoundManager.PlaySound(SoundManager.Sound.ButtonClick); });
        timerLevelButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.TimerLevel); SoundManager.PlaySound(SoundManager.Sound.ButtonClick); });

        instructionsButton.onClick.AddListener(ShowInstructionsPanel);
        quitButton.onClick.AddListener(Application.Quit);

        quitInstructionsPanelButton.onClick.AddListener(HideInstructionsPanel);

        HideInstructionsPanel();
        HideChooseLevelPanel();
        HideResetHighScorePanel();

        SoundManager.CreateSoundManagerGameObject();
    }

    private void ShowInstructionsPanel()
    {
        instructionsPanel.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
    }

    private void HideInstructionsPanel()
    {
        instructionsPanel.SetActive(false);
        SoundManager.PlaySound(SoundManager.Sound.ButtonOver);
    }

    private void ShowChooseLevelPanel()
    {
        chooseLevelPanel.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
    }

    private void HideChooseLevelPanel()
    {
        chooseLevelPanel.SetActive(false);
    }

    private void HideResetHighScorePanel()
    {
        resetHighScorePanel.SetActive(false);
    }

    private void ShowResetHighScorePanel()
    {
        resetHighScorePanel.SetActive(true);
    }

    private void ResetHighScore()
    {
        PlayerPrefs.DeleteAll();
        ScoreUI.Instance.UpdateHighScoreText();
    }

    private void ButtonResetHighScore()
    {
        HideResetHighScorePanel();
        ShowChooseLevelPanel();
        ResetHighScore();
    }

    private void ButtonNoResetHighScore()
    {
        HideResetHighScorePanel();
        ShowChooseLevelPanel();
    }
}
