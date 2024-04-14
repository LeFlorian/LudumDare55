using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public float secondBeforeSwitch = 8f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Switch());
    }

    IEnumerator Switch()
    {
        yield return new WaitForSeconds(secondBeforeSwitch);
        SceneManager.LoadScene(1);
    }
}
