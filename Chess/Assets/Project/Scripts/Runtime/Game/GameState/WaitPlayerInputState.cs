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
    }

    public override void Exit()
    {
        inputHandler.playetTouched -= PlayerTouch;

    }

    public override void Update()
    {
        
    }

    private void PlayerTouch(Vector2 vector2)
    {
        Ray ray = camera.ScreenPointToRay(vector2);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            if(hitInfo.transform.TryGetComponent(out Tile tile))
            {
                if (tile.HasFigure() && tile.figure.team == stateController.curentMovingTeam)
                {
                    stateController.activeTile = tile;
                    stateController.ChangeState(stateController.figureMoveState);
                }
            }
        }
    }
}
