using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager inst;

    private GameControls gameControl;

    public UnityEvent<Vector2> touchStart = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> touchMove = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> touchEnd = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> move = new UnityEvent<Vector2>();

    private void Awake()
    {
        inst = this;
        gameControl = new GameControls();
    }

    private void OnEnable()
    {
        gameControl.Enable();
    }

    private void OnDisable()
    {
        gameControl.Disable();
    }

    private void Start()
    {
        gameControl.Game.TouchPressed.started += ctx => OnTouchStart(ctx);
        gameControl.Game.TouchPosition.performed += ctx => OnTouchMove(ctx);
        gameControl.Game.TouchPressed.canceled += ctx => OnTouchEnd(ctx);

        gameControl.Game.Move.performed += ctx => OnMove(ctx);
        gameControl.Game.Move.canceled += ctx => OnMove(ctx);
    }

    private void OnTouchStart(InputAction.CallbackContext context)
    {
        touchStart.Invoke(gameControl.Game.TouchPosition.ReadValue<Vector2>());
    }

    private void OnTouchMove(InputAction.CallbackContext context)
    {
        touchMove.Invoke(gameControl.Game.TouchPosition.ReadValue<Vector2>());
    }

    private void OnTouchEnd(InputAction.CallbackContext context)
    {
        touchEnd.Invoke(gameControl.Game.TouchPosition.ReadValue<Vector2>());
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        move.Invoke(gameControl.Game.Move.ReadValue<Vector2>());
    }

}
