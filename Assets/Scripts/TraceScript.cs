using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceScript : MonoBehaviour
{
   
    public GameObject waterPuddlePrefab;

    private void Update()
    {
        // Raycast vers le bas depuis la position des pieds du personnage
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Instancier un préfab de flaques d'eau à la position de contact avec le sol
            Instantiate(waterPuddlePrefab, hit.point, Quaternion.identity);
        }
    }
    }

