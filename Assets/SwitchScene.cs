using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public float secondBeforeSwitch = 8f;

    public GameObject panel1;
    public GameObject panel2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Switch());
    }

    IEnumerator Switch()
    {
        panel1.SetActive(true);
        yield return new WaitForSeconds(secondBeforeSwitch);

        panel1.SetActive(false);
        panel2.SetActive(true);

        yield return new WaitForSeconds(secondBeforeSwitch);


        SceneManager.LoadScene(1);
    }
}
