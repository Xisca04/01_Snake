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

    [SerializeField] private GameObject instructionsPanel;

    private void Awake()
    {
        playButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.Game); SoundManager.PlaySound(SoundManager.Sound.ButtonClick); });
        instructionsButton.onClick.AddListener(ShowInstructionsPanel);
        quitButton.onClick.AddListener(Application.Quit);

        quitInstructionsPanelButton.onClick.AddListener(HideInstructionsPanel);

        HideInstructionsPanel();

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
}
