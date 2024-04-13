using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timerDisplay;
    private string time;
    private DateTime startTime;
    private float timeS;
    private void Start()
    {
        startTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        timeS += Time.deltaTime;
        
        TimeSpan diff = DateTime.Now.Subtract(startTime);

        timerDisplay.text = diff.ToString(@"mm\:ss\.fff");
    }
}
