using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPlayerInputState : GameStateBase
{
    private InputHandler inputHandler;
    private Camera camera;
    private GameStateController stateController;
    public WaitPlayerInputState(InputHandler inputHandler,GameStateController stateController, Camera camera)
    {
        this.inputHandler = inputHandler;
        this.camera = camera;
        this.stateController = stateController;
    }
    public override void Enter()
    {
        inputHandler.playetTouched += PlayerTouch;
        inputHandler.startPress += StartDrag;
    }

    public override void Exit()
    {
        inputHandler.playetTouched -= PlayerTouch;
        inputHandler.startPress -= StartDrag;

    }

    public override void Update()
    {
        
    }

    private void PlayerTouch(Vector2 mousePosition)
    {
        if (IsPlayerTouchTile(mousePosition))
            stateController.ChangeState(stateController.figureMoveState);
    }

    private void StartDrag(Vector2 mousePosition)
    {
        if (IsPlayerTouchTile(mousePosition))
        {
            stateController.ChangeState(stateController.figureDragAndDropState);
            stateController.startMousePosition = mousePosition;
        }
    }

    private bool IsPlayerTouchTile(Vector2 mousePosition)
    {
        Ray ray = camera.ScreenPointToRay(mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            if (hitInfo.transform.TryGetComponent(out Tile tile) && tile.HasFigure() && tile.figure.team == stateController.curentMovingTeam)
            {
                stateController.activeTile = tile;
                return true;
            }
        }
        return false;
    }
}
