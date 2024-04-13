using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoutteSpawner : MonoBehaviour
{
    public GameObject gouttes;
    public float waitTime;

    private void Start()
    {
        StartCoroutine(SpawnGouttes()); 
    }
    IEnumerator SpawnGouttes()
    {
        while (true)
        {
            float size = (float)(Random.value * 0.5);
            Vector3 goutteSize = new Vector3(size, size, size);
            yield return new WaitForSeconds(waitTime); 
            GameObject newGoutte = Instantiate(gouttes);
            newGoutte.transform.localScale = goutteSize;
            newGoutte.transform.position = transform.position; 
            
            newGoutte.GetComponent<Rigidbody>().AddForce(transform.forward * (Random.value * 100));
        }
    }
}
