using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timerDisplay;
   /*
    
    private string time;
    private DateTime startTime;
    private float timeS; 
    */
    
    public float timerDuration = 20f; // Durée du timer en secondes
    private float currentTime = 0f;
    public bool timerActive = false;
    private void Start()
    {
        StartTimer();

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
            currentTime += Time.deltaTime; // Incrémente le temps écoulé depuis la dernière frame
            timerDisplay.text = currentTime.ToString(CultureInfo.CurrentCulture).Split(",")[0];
            // Vérifie si le temps écoulé est supérieur ou égal à la durée du timer
            if (currentTime >= timerDuration)
            {
                // Le timer est écoulé, effectuez les actions nécessaires ici
                Debug.Log("Le timer de 20 secondes est écoulé !");
                // Arrête le timer
                StopTimer();
            }
        }
        
        
        
    }
    public void StartTimer()
    {
        // Active le timer et réinitialise le temps écoulé
        timerActive = true;
        currentTime = 0f;
    }

    public void StopTimer()
    {
        // Arrête le timer en le désactivant
        timerActive = false;
    }
    
}
