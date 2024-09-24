using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureMoveState : GameStateBase
{
    protected InputHandler inputHandler;
    protected Camera camera;
    protected GameStateController stateController;
    protected CheckController checkController;

    protected Tile activeTile;
    protected List<Tile> figurePathList;
    protected List<Tile> figureAttackList;

    public FigureMoveState(InputHandler inputHandler, GameStateController stateController, Camera camera, CheckController checkController)
    {
        this.inputHandler = inputHandler;
        this.stateController = stateController;
        this.camera = camera;
        this.checkController = checkController;
    }
    public override void Enter()
    {
        activeTile = stateController.activeTile;

        activeTile.SelectMarkerSetActive(true);
        figurePathList = checkController.GetFigurePath(activeTile.figure);
        figureAttackList = checkController.GetAttackTiles(activeTile.figure);
        
        ActiveFigureTiles(true);

        inputHandler.playetTouched += PlayerTouch;
        inputHandler.startPress += StartDrag;

    }

    public override void Exit()
    {
        ActiveFigureTiles(false);
        activeTile.SelectMarkerSetActive(false);
        figurePathList.Clear();
        figureAttackList.Clear();

        inputHandler.playetTouched -= PlayerTouch;
        inputHandler.startPress -= StartDrag;

    }

    public override void Update()
    {
        
    }

    private void PlayerTouch(Vector2 vector2)
    {
        Ray ray = camera.ScreenPointToRay(vector2);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100) && hitInfo.transform.TryGetComponent(out Tile touchedTile))
        {
            if (touchedTile.HasFigure())
            {
                if (touchedTile.figure.team == activeTile.figure.team)
                {
                    if(touchedTile.figure == activeTile.figure)
                    {
                        activeTile.SelectMarkerSetActive(false);
                        stateController.ChangeState(stateController.waitPlayerInputState);
                        return;
                    }
                    stateController.activeTile.SelectMarkerSetActive(false);
                    stateController.activeTile = touchedTile;
                    stateController.ChangeState(stateController.figureMoveState);
                    return;
                }
                else
                    FigureAttack(touchedTile);
            }
            else
                MoveFigure(touchedTile);
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
    private void StartDrag(Vector2 mousePosition)
    {
        if (IsPlayerTouchTile(mousePosition))
        {
            stateController.ChangeState(stateController.figureDragAndDropState);
            stateController.startMousePosition = mousePosition;
        }
    }

    protected bool MoveFigure(Tile touchedTile)
    {
        foreach (Tile tile in figurePathList)
        {
            if (touchedTile == tile)
            {
                ChangeFigurePosition(touchedTile);
                return true;
            }
        }
        activeTile.SelectMarkerSetActive(false);
        stateController.ChangeState(stateController.waitPlayerInputState);
        return false;
    }

    protected bool FigureAttack(Tile touchedTile)
    {
        foreach(Tile tile in figureAttackList)
        {
            if(touchedTile == tile)
            {
                checkController.RemoveFigure(touchedTile.figure);
                Object.Destroy(touchedTile.figure.gameObject);

                ChangeFigurePosition(touchedTile);

                return true;
            }
        }
        activeTile.SelectMarkerSetActive(false);
        stateController.ChangeState(stateController.waitPlayerInputState);
        return false;
    }

    protected void ChangeFigurePosition(Tile touchedTile)
    {
        activeTile.figure.MoveTo(touchedTile.xPos, touchedTile.zPos);
        activeTile.SelectMarkerSetActive(false);
        stateController.ChangeTeam();

        touchedTile.figure = activeTile.figure;
        stateController.activeTile.figure = null;

        ActiveFigureTiles(false);
        stateController.ChangeState(stateController.waitPlayerInputState);

        checkController.IsKingAttacked(touchedTile.figure);
    }

    private void ActiveFigureTiles(bool isActive)
    {
        foreach (Tile tile in figurePathList)
            tile.MoveMarkerSetActive(isActive);
        foreach (Tile tile in figureAttackList)
            tile.AttackMarkerSetActive(isActive);
    }
}
