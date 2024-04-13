using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
            Destroy(GetComponent<Rigidbody>());
    }
}
