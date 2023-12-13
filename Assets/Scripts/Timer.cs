using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


public class Timer: MonoBehaviour
{
    // Timer of timer level
    public static Timer Instance { get; private set; }  // Singleton

    public float timeLeft = 60f;
    private bool timerOn = false;

    public float timerFood = 5f;
    public TextMeshProUGUI timerText;
    
    private float timerPanel = 0.50f;
    [SerializeField] private GameObject timerFoodPanel;

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
        timerOn = true;
        timerFoodPanel.SetActive(false);
    }

    private void Update()
    {
        CountDown();
    }

    private void CountDown() // Timer
    {
        if (timerOn)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }

            if(timeLeft <= 0)
            {
                timeLeft = 0;
                timerOn = false;
            }

            if (timeLeft < 5)
            {
                timerText.color = Color.red;
            }
        }
    }

    private void UpdateTimer (float currentTime) // Convert the 60 seconds to 1 min
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    public IEnumerator AddedTimeFood() // The panel of the 5 seconds achieved appears and disappears
    {
        timerFoodPanel.SetActive(true);
        yield return new WaitForSeconds(timerPanel);
        timerFoodPanel.SetActive(false);
    }
    
}
