using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MenuPrincipal,
    Playing,
    End
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;

    public GameObject parentObject;
    Transform[] allTesters;

    public float tolerancePrecision = 2f;
    public float toleranceCompletion = 90f;

    [Header("Functionnal")]
    public float allDistCumulate;
    public int numberOFGoutte;
    public float precision;
    public float completion;

    List<Transform> testers = new List<Transform>();

    public GameObject goutteCollector;

    private void Awake()
    {
        Instance = this;

        if (!PlayerPrefs.HasKey("bestCompletion"))
        {
            PlayerPrefs.SetFloat("bestCompletion", 0);
            PlayerPrefs.SetFloat("bestPrecision", 0);
        }
    }

    private void Start()
    {
        allTesters = parentObject.GetComponentsInChildren<Transform>();

        goutteCollector = new GameObject();
        goutteCollector.name = "goutteCollector";
    }

    public void AddingScoreByPosition(Vector3 pos)
    {
        numberOFGoutte++;

        float minDist = 200;
        Transform minDistObject = null;

        foreach (Transform t in allTesters)
        {
            float dist = Vector3.Distance(pos,t.position);
            if (dist < minDist)
            {
                minDist = dist;
                minDistObject = t;
            }
        }

        if (!testers.Contains(minDistObject))
        {
            testers.Add(minDistObject);
        }

        float adding = Mathf.InverseLerp(tolerancePrecision, 0f, minDist);

        allDistCumulate += adding;
        precision = allDistCumulate / numberOFGoutte;

        if (State != GameState.End)
        {
            if (TestFinishDrawing())
                EndGame();
        }
    }

    private bool TestFinishDrawing()
    {
        bool asFinishDrawing = false;

        int max = allTesters.Length;
        int amount = 0;
        
        foreach (Transform t in allTesters)
        {
            foreach (Transform t2 in testers)
            {
                if (t2 == t)
                {
                    amount++;
                }
            }
        }

        float realCompletion = (float)amount / max;

        if (realCompletion >= toleranceCompletion/100f)
        {
            asFinishDrawing = true;
        }

        completion = Mathf.InverseLerp(0, toleranceCompletion / 100, realCompletion);

        return asFinishDrawing;
    }

    public void LaunchGame()
    {
        Debug.Log("LaunchGame");


        precision = 0;
        completion = 0;
        allDistCumulate = 0;
        numberOFGoutte = 0;
        testers.Clear();

        State = GameState.Playing;

        Transform[] gouttes = goutteCollector.GetComponentsInChildren<Transform>();
        foreach (Transform t in gouttes)
        {
            if (t.gameObject != goutteCollector)
            {

                Destroy(t.gameObject);
            }
        }

        Timer.instance.StartTimer();

        StartCoroutine(FindAnyObjectByType<GoutteSpawner>().SpawnGouttes());


    }

    public void EndGame()
    {
        

        Timer.instance.StopTimer();
        State = GameState.End;

        float currentBestCompletion = PlayerPrefs.GetFloat("bestCompletion");
        float currentBestPrecision = PlayerPrefs.GetFloat("bestPrecision");

        if (completion >= currentBestCompletion)
        {
            PlayerPrefs.SetFloat("bestCompletion", completion);

            if (completion == currentBestCompletion)
            {
                if (precision > currentBestPrecision)
                    PlayerPrefs.SetFloat("bestPrecision", precision);
            }
            else
            {
                PlayerPrefs.SetFloat("bestPrecision", precision);
            }
        }

        MenuController.instance.EndGame(completion, precision);



    Debug.Log("End game");
    }
}
