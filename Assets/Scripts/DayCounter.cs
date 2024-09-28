using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayCounter : MonoBehaviour
{
    public static DayCounter Instance { get; private set; }
    public TextMeshProUGUI scoreText;

    private int currentDay = 1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes
    }

    private void Start()
    {
        UpdateDay(currentDay);
    }

    public void UpdateDay(int dayCounter)
    {
        currentDay = dayCounter;
        scoreText.text = "Day " + currentDay;
    }

    // Optional: Method to increment day
    public void NextDay()
    {
        UpdateDay(currentDay + 1);
    }
}
