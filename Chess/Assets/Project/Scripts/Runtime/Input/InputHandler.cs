using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private GameInput gameInput;

    public event Action<Vector2> playetTouched;
    void Start()
    {
        gameInput = new GameInput();
        gameInput.Gameplay.Enable();
        gameInput.Gameplay.Click.performed += OnClick;
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        gameInput.Gameplay.Disable();
    }
    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 pos = context.action.ReadValue<Vector2>();

        playetTouched?.Invoke(pos);
    }
}
