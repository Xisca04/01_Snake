using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public static PauseUI Instance { get; private set; }  // Singleton

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance Pause UI");
        }

        Instance = this;

        mainMenuButton.onClick.AddListener(() => { Time.timeScale = 1f; Loader.Load(Loader.Scene.MainMenu); });
        resumeButton.onClick.AddListener(() => { GameManager.Instance.ResumeGame(); });

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
