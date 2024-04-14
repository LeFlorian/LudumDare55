using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Goutte")
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            RaycastHit hit;
            if (Physics.Raycast(transform.position, -rb.velocity, out hit))
            {
                transform.position = hit.point;
            }


            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());



        }
    }
}
