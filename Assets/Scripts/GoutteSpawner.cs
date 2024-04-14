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
    public Rigidbody playerRB;

    public IEnumerator SpawnGouttes()
    {
        Debug.Log("SpawningGoutte !");

        while (Timer.instance.timerActive)
        {
            float size = (float)(Random.value * 0.5);
            Vector3 goutteSize = new Vector3(size, (float)0.1, size);
            yield return new WaitForSeconds(waitTime); 
            GameObject newGoutte = Instantiate(gouttes);
            newGoutte.transform.localScale = goutteSize;
            newGoutte.transform.position = transform.position;
            newGoutte.transform.parent = GameManager.Instance.goutteCollector.transform;
            
            newGoutte.GetComponent<Rigidbody>().AddForce(transform.forward * (Random.value * 100) + playerRB.velocity);
        }
    }

}
