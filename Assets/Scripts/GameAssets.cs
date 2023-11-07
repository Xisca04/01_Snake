using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    public static GameAssets Instance { get; private set; }  // Singleton

    public Sprite snakeHeadSprite;
    public Sprite foodSprite;
    public Sprite snakeBodySprite;

    public AudioClip buttonClickClip; 
    public AudioClip buttonOverClip;
    public AudioClip snakeDieClip;
    public AudioClip snakeEatClip;
    public AudioClip snakeMoveClip;

    public SoundAudioClip[] soundAudioClipArray;

    private void Awake() // Singleton
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance Game Assets");
        }

        Instance = this;
    }
}
