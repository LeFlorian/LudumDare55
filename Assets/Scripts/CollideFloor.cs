using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.up, out hit))
            {
                if (other.gameObject.tag == "Floor")
                {
                    transform.position = hit.point;
                }
            }
            else
            {
                if (Physics.Raycast(transform.position, -Vector3.up, out hit))
                {
                    if (other.gameObject.tag == "Floor")
                    {
                        transform.position = hit.point;
                    }
                }
            }


            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());

            GameManager.Instance.AddingScoreByPosition(transform.position);

        }
    }
}
