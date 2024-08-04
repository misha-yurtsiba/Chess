using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private GameInput gameInput;
    void Awake()
    {
        gameInput = new GameInput();
        gameInput.Gameplay.Click.performed += OnClick;
    }
    private void OnEnable()
    {
        gameInput.Gameplay.Enable();
    }
    private void OnDisable()
    {
        gameInput.Gameplay.Disable();
    }
    private void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("Click");
        Vector2 pos = context.action.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(pos);

        if(Physics.Raycast(ray,out RaycastHit hitInfo, 100))
        {
            if(hitInfo.transform.TryGetComponent(out Tile tile))
            {
                tile.Active();
            }
        }
    }
}
