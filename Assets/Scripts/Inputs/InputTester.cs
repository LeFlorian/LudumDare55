using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTester : MonoBehaviour
{
    private void Start()
    {
        InputManager.inst.move.AddListener(OnTouchStart);
    }

    public void OnTouchStart(Vector2 position)
    {
        Debug.Log(position);
    }
}
