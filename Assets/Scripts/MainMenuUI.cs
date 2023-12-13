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

    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject chooseLevelPanel;

    private void Awake()
    {
        SoundManager.CreateSoundManagerGameObject();

        // Programming the main menu buttons by code
        playButton.onClick.AddListener(ShowChooseLevelPanel);

        // Load the scene Game (original level) and play a sound
        originalLevelButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.Game); SoundManager.PlaySound(SoundManager.Sound.ButtonClick); });
        // Load the scene TimerLevel and play a sound
        timerLevelButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.TimerLevel); SoundManager.PlaySound(SoundManager.Sound.ButtonClick); });

        instructionsButton.onClick.AddListener(ShowInstructionsPanel);
        quitButton.onClick.AddListener(Application.Quit);

        quitInstructionsPanelButton.onClick.AddListener(HideInstructionsPanel);

        HideInstructionsPanel();
        HideChooseLevelPanel();
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
}
