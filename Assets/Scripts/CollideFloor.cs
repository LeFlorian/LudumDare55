using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideFloor : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
        {
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());

        }
    }
}
