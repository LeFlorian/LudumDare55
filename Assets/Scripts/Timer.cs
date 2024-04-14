using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Timer : MonoBehaviour
{
    public static Timer instance; 
    public TextMeshProUGUI timerDisplay;
   /*
    
    private string time;
    private DateTime startTime;
    private float timeS; 
    */
    
    public float timerDuration; // Durée du timer en secondes
    private float _currentTime;
    public bool timerActive = false;

    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       /*
        timeS += Time.deltaTime;
        
        TimeSpan diff = DateTime.Now.Subtract(startTime);

        timerDisplay.text = diff.ToString(@"mm\:ss\.fff");
        */
       
        if (timerActive)
        {
            _currentTime += Time.deltaTime; // Incrémente le temps écoulé depuis la dernière frame
            timerDisplay.text = _currentTime.ToString(CultureInfo.CurrentCulture).Split(",")[0];
            // Vérifie si le temps écoulé est supérieur ou égal à la durée du timer
            if (_currentTime >= timerDuration)
            {
                GameManager.Instance.EndGame();
            }
        }
        
        
        
    }
    public void StartTimer()
    {
        // Active le timer et réinitialise le temps écoulé
        timerActive = true;
        _currentTime = 0f;
    }

    public void StopTimer()
    {
        // Arrête le timer en le désactivant
        timerActive = false;
    }
    
}
