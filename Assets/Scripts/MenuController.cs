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

        bestCompletion.text = $"Completion: {FormatValue(PlayerPrefs.GetFloat("bestCompletion"))}%";
        bestPrecision.text = $"Precision:  {FormatValue(PlayerPrefs.GetFloat("bestPrecision"))}%";
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

        currentCompletion.text = $"Completion: {FormatValue(completion)}%";
        currentPrecision.text = $"Precision:  {FormatValue(precision)}%";

        bestCompletion.text = $"Completion:  {FormatValue(PlayerPrefs.GetFloat("bestCompletion"))}%";
        bestPrecision.text = $"Precision:  {FormatValue(PlayerPrefs.GetFloat("bestPrecision"))}%";
    }

    private float FormatValue(float value)
    {
        return Mathf.Round(value * 100 * 100) / 100;
    }
}
