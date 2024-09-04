using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureMoveState : GameStateBase
{
    private InputHandler inputHandler;
    private Camera camera;
    private GameStateController stateController;
    private CheckController checkController;

    private Tile activeTile;
    private List<Tile> figurePathList;
    private List<Tile> figureAttackList;

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
    }

    public override void Exit()
    {
        ActiveFigureTiles(false);
        figurePathList.Clear();
        figureAttackList.Clear();

        inputHandler.playetTouched -= PlayerTouch;
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

    private void MoveFigure(Tile touchedTile)
    {
        foreach (Tile tile in figurePathList)
        {
            if (touchedTile == tile)
            {
                ChangeFigurePosition(touchedTile);
                return;
            }
        }
        activeTile.SelectMarkerSetActive(false);
        stateController.ChangeState(stateController.waitPlayerInputState);

    }

    private void FigureAttack(Tile touchedTile)
    {
        foreach(Tile tile in figureAttackList)
        {
            if(touchedTile == tile)
            {
                checkController.RemoveFigure(touchedTile.figure);
                Object.Destroy(touchedTile.figure.gameObject);

                ChangeFigurePosition(touchedTile);

                return;
            }
        }
        activeTile.SelectMarkerSetActive(false);
        stateController.ChangeState(stateController.waitPlayerInputState);
    }

    private void ChangeFigurePosition(Tile touchedTile)
    {
        activeTile.figure.MoveTo(touchedTile.xPos, touchedTile.zPos);
        activeTile.SelectMarkerSetActive(false);
        ChangeTeam();

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
    private void ChangeTeam()
    {
        stateController.curentMovingTeam = (stateController.curentMovingTeam == Team.White)
                    ? Team.Black : Team.White;
    }
}
