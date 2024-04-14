using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject parentObject;
    Transform[] allTesters;

    public float tolerance = 2f;

    [Header("Functionnal")]
    public float allDistCumulate;
    public int numberOFGoutte;
    public float score;

    List<Transform> testers = new List<Transform>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        allTesters = parentObject.GetComponentsInChildren<Transform>();
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

        float adding = Mathf.InverseLerp(tolerance, 0f, minDist);

        allDistCumulate += adding;
        score = allDistCumulate / numberOFGoutte;

        if (TestFinishDrawing())
            EndGame();
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

        if ((float)amount/max >= 0.90f)
        {
            asFinishDrawing = true;
        }

        return asFinishDrawing;
    }

    private void EndGame()
    {
        Debug.Log("End game");
    }
}
