using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureMoveState : GameStateBase
{
    private InputHandler inputHandler;
    private Camera camera;
    private GameStateController stateController;
    private CheckAndMateController checkAndMateController;

    private Tile activeTile;
    private List<Tile> figurePathList;
    private List<Tile> figureAttackList;

    public FigureMoveState(InputHandler inputHandler, GameStateController stateController, Camera camera, CheckAndMateController checkAndMateController)
    {
        this.inputHandler = inputHandler;
        this.stateController = stateController;
        this.camera = camera;
        this.checkAndMateController = checkAndMateController;
    }
    public override void Enter()
    {
        activeTile = stateController.activeTile;

        activeTile.SelectMarkerSetActive(true);
        figurePathList = checkAndMateController.GetFigurePath(activeTile.figure, activeTile);
        figureAttackList = checkAndMateController.GetAttackTiles(activeTile.figure);
        
        if(figurePathList != null)
            foreach (Tile tile in figurePathList)           
                tile.MoveMarkerSetActive(true);

        if (figureAttackList != null)
            foreach (Tile tile in figureAttackList)
                tile.AttackMarkerSetActive(true);

        inputHandler.playetTouched += PlayerTouch;
    }

    public override void Exit()
    {
        foreach(Tile tile in figurePathList)
            tile.MoveMarkerSetActive(false);
        
        foreach (Tile tile in figureAttackList)
            tile.AttackMarkerSetActive(false);
        

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
                checkAndMateController.RemoveFigure(touchedTile.figure);
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

        foreach (Tile tile in figureAttackList)
            tile.AttackMarkerSetActive(false);

        checkAndMateController.IsKingAttacked(touchedTile.figure);

        stateController.ChangeState(stateController.waitPlayerInputState);
    }
    private void ChangeTeam()
    {
        stateController.curentMovingTeam = (stateController.curentMovingTeam == Team.White)
                    ? Team.Black : Team.White;
    }
}
