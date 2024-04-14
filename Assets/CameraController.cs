using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 baseDiff;
    private GameObject go;

    private PlayerController pc;

    private void Start()
    {
        go = new GameObject("FollowCam");
        pc = FindObjectOfType<PlayerController>();
        baseDiff = this.transform.position - pc.transform.position;
    }

    private void FixedUpdate()
    {
        go.transform.position = pc.transform.position + baseDiff;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, go.transform.position, 2f * Time.deltaTime);
    }
}
