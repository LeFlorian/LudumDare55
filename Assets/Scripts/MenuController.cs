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

        bestCompletion.text = $"{PlayerPrefs.GetFloat("bestCompletion") * 100}%";
        bestPrecision.text = $"{PlayerPrefs.GetFloat("bestPrecision") * 100}%";
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

        currentCompletion.text = $"{completion * 100}%";
        currentPrecision.text = $"{precision * 100}%";

        bestCompletion.text = $"{PlayerPrefs.GetFloat("bestCompletion") * 100}%";
        bestPrecision.text = $"{PlayerPrefs.GetFloat("bestPrecision") * 100}%";
    }
}
