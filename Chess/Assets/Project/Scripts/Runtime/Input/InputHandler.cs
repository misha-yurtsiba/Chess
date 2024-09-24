using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public event Action<Vector2> playetTouched;
    public event Action<Vector2> startPress;
    public event Action<Vector2> endPress;
    public event Action<Vector2> moving;

    [SerializeField] private float pressTime = 0.3f;

    private GameInput gameInput;

    private bool isPressed;
    private bool isMoving;
    private float curentPressTime;
    void Start()
    {
        isPressed = false;
        isMoving = false;
    }
    private void OnEnable()
    {
        gameInput = new GameInput();
        gameInput.Gameplay.Enable();

        gameInput.Gameplay.Press.started += OnPress;
        gameInput.Gameplay.Press.canceled += OnEndPress;
    }
    private void OnDisable()
    {
        gameInput.Gameplay.Press.started -= OnPress;
        gameInput.Gameplay.Press.canceled -= OnEndPress;

        gameInput.Gameplay.Disable();
    }

    private void Update()
    {
        if (!isPressed) return;

        if(curentPressTime < pressTime)
        {
            curentPressTime +=Time.deltaTime;
        }
        else if(curentPressTime >= pressTime && !isMoving)
        {
            isMoving = true;
            startPress?.Invoke(ReadVector2());
        }
        else
        {
            moving?.Invoke(ReadVector2());
        }
    }
    private Vector3 ReadVector2()
    {
        return gameInput.Gameplay.Position.ReadValue<Vector2>();
    }
    private void OnPress(InputAction.CallbackContext context) 
    {
        isPressed = true;
        curentPressTime = 0;
    }
    private void OnEndPress(InputAction.CallbackContext context)
    {
        if (curentPressTime < pressTime)
            playetTouched?.Invoke(ReadVector2());

        isPressed = false;
        isMoving = false;

        curentPressTime = 0;
        endPress?.Invoke(ReadVector2());
    }
}

