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
        while (Timer.instance.timerActive)
        {
            float size = Mathf.Clamp((float)(Random.value * 0.3),0.1f,0.3f);
            Vector3 goutteSize = new Vector3(size, (float)0.03, size);
            yield return new WaitForSeconds(waitTime); 
            GameObject newGoutte = Instantiate(gouttes);
            newGoutte.transform.localScale = goutteSize;
            newGoutte.transform.position = transform.position;
            newGoutte.transform.parent = GameManager.Instance.goutteCollector.transform;
            
            newGoutte.GetComponent<Rigidbody>().AddForce(transform.forward * (Random.value * 100) + playerRB.velocity*2);
        }
    }

}
