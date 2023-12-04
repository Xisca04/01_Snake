using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEditor.ShaderGraph;

public class Timer: MonoBehaviour
{
    // Timer of timer level
    public static Timer Instance { get; private set; }  // Singleton

    public float _timer = 60f;
    public float timerFood = 5f;
    public TextMeshProUGUI timerText;

    [SerializeField] private GameObject timerFoodPanel;

    private float timerPanel = 3f;

    private void Awake() // Singleton
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Instance Timer");
        }

        Instance = this;
    }

    private void Start()
    {
        _timer = 60;
        timerFoodPanel.SetActive(false);
    }

    private void Update()
    {
        CountDown();
    }

    private void CountDown() // Timer
    {
        _timer -= Time.deltaTime;
        timerText.text = "" + _timer.ToString("f1");

        if (_timer <= 0)
        {
            _timer = 0;
        }

        if(_timer < 5)
        {
            timerText.color = Color.red;
        }
    }

    
    public IEnumerator AddedTimeFood()
    {
        timerFoodPanel.SetActive(true);
        yield return new WaitForSeconds(timerPanel);
        timerFoodPanel.SetActive(false);
    }
    
}
