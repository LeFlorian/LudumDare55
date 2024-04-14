using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    public GameObject canvasMenuPrincipal;

    public TextMeshProUGUI bestCompletion;
    public TextMeshProUGUI bestPrecision;

    public GameObject currentScore;
    public TextMeshProUGUI currentCompletion;
    public TextMeshProUGUI currentPrecision;


    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {

        bestCompletion.text = $"{FormatValue(PlayerPrefs.GetFloat("bestCompletion"))}%";
        bestPrecision.text = $"{FormatValue(PlayerPrefs.GetFloat("bestPrecision"))}%";
    }

    public void Play()
    {
        GameManager.Instance.LaunchGame();
        canvasMenuPrincipal.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void EndGame(float completion, float precision)
    {
        canvasMenuPrincipal.SetActive(true);

        currentScore.SetActive(true);

        currentCompletion.text = $"{FormatValue(completion)}%";
        currentPrecision.text = $"{FormatValue(precision)}%";

        bestCompletion.text = $"{FormatValue(PlayerPrefs.GetFloat("bestCompletion"))}%";
        bestPrecision.text = $"{FormatValue(PlayerPrefs.GetFloat("bestPrecision"))}%";
    }

    private float FormatValue(float value)
    {
        return Mathf.Round(value * 100 * 100) / 100;
    }
}
