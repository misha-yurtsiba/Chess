using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureDragAndDropState : FigureMoveState
{
    private Camera camera;
    private Figure movingFigure;
    private Plane dragPlane;
    private Vector3 targetPosition;
    private float yOffset;

    public FigureDragAndDropState(InputHandler inputHandler, GameStateController stateController, Camera camera, CheckController checkController, float yOffset) : base(inputHandler, stateController, camera, checkController)
    {
        this.yOffset = yOffset;
    }

    public override void Enter()
    {
        base.Enter();
        camera = Camera.main;
        inputHandler.endPress += EndDragAndDrop;
        inputHandler.moving += UpdateFigurePosition;

        StartDragAndDrop();

        Debug.Log("Start DragAndDrop");
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        base.Exit();
        inputHandler.endPress -= EndDragAndDrop;
        inputHandler.moving -= UpdateFigurePosition;

    }
    private void StartDragAndDrop()
    {
        movingFigure = activeTile.figure;
        dragPlane = new Plane(camera.transform.forward, movingFigure.transform.position + new Vector3(0,yOffset,0));

        Ray ray = camera.ScreenPointToRay(stateController.startMousePosition);
        float dist;
        dragPlane.Raycast(ray, out dist);
    }
    private void UpdateFigurePosition(Vector2 mousePosition)
    {
        Ray ray = camera.ScreenPointToRay(mousePosition);

        float dist;
        dragPlane.Raycast(ray, out dist);
        movingFigure.transform.position = ray.GetPoint(dist);
    }
    private void EndDragAndDrop(Vector2 mousePosition)
    {
        Ray ray = camera.ScreenPointToRay(mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100) && hitInfo.transform.TryGetComponent(out Tile touchedTile))
        {
            if (touchedTile.HasFigure())
            {
                if (touchedTile.figure.team == activeTile.figure.team)
                {
                    if (touchedTile.figure == activeTile.figure)
                    {
                        activeTile.SelectMarkerSetActive(false);
                        stateController.ChangeState(stateController.waitPlayerInputState);
                        ResetPos();
                        return;
                    }
                    stateController.activeTile.SelectMarkerSetActive(false);
                    stateController.ChangeState(stateController.waitPlayerInputState);
                    ResetPos();
                    return;
                }
                else
                    if (!FigureAttack(touchedTile))
                    ResetPos();
            }
            else
                if (!MoveFigure(touchedTile))
                ResetPos();
        }
        else
        {
            stateController.activeTile.SelectMarkerSetActive(false);
            stateController.ChangeState(stateController.waitPlayerInputState);
            ResetPos();
        }
    } 

    private void ResetPos()
    {
        movingFigure.transform.position = new Vector3(activeTile.xPos, movingFigure.transform.position.y - yOffset, activeTile.zPos);
    }
}
