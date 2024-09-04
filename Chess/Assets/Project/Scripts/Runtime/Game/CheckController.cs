using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckController: IDisposable
{
    public List<Figure> whiteFigures = new List<Figure>();
    public List<Figure> blackFigures = new List<Figure>();

    private List<Tile> curentPath = new List<Tile>();
    private List<Tile> curentAttackTiles = new List<Tile>();

    private bool isCheck = false;

    private Figure kingAttackedFigure;
    private King whiteKing;
    private King blackKing;
    private King checkedKing;
    private Checkmate checkmate;
    private IRestart restartGame;

    public CheckController(Checkmate checkmate, IRestart restartGame)
    {
        this.checkmate = checkmate;
        this.restartGame = restartGame;

        restartGame.restart += RemoveAllFigure;
    }

    public void Dispose() => restartGame.restart -= RemoveAllFigure;

    public void SetKing(King king)
    {
        if (king.team == Team.White)
            whiteKing = king;
        else
            blackKing = king;
    }
    public List<Tile> GetFigurePath(Figure movingFigure)
    {
        curentPath.Clear();

        Tile movingFigureCurentTile = movingFigure.GetTile();
        movingFigureCurentTile.figure = null;
        List<Tile> figurePaht = movingFigure.GetMoveTiles();

        foreach (Tile tile in figurePaht)
        {
            tile.figure = movingFigure;

            if (!IsFigureAttackKing(movingFigure.team, null))
                curentPath.Add(tile);

            tile.figure = null;
        }

        movingFigureCurentTile.figure = movingFigure;
        return curentPath;
    }

    public List<Tile> GetAttackTiles(Figure movingFigure)
    {
        curentAttackTiles.Clear();
        List<Tile> figureAttackTiles = movingFigure.GetAttackTiles();

        Tile movingFigureCurentTile = movingFigure.GetTile();
        movingFigureCurentTile.figure = null;
        
        if (isCheck)
        {
            foreach (Tile tile in figureAttackTiles)
            {
                Figure figure = tile.figure;
                tile.figure = movingFigure;

                if (figure == kingAttackedFigure && !IsFigureAttackKing(movingFigure.team, figure))
                    curentAttackTiles.Add(tile);

                tile.figure = figure;
            }
        }
        else
        {
            foreach (Tile tile in figureAttackTiles)
            {
                Figure figure = tile.figure;
                tile.figure = movingFigure;

                if (!IsFigureAttackKing(movingFigure.team, figure))
                    curentAttackTiles.Add(tile);
                tile.figure = figure;
            }
        }

        movingFigureCurentTile.figure = movingFigure;
        return curentAttackTiles;

    }

    public void IsKingAttacked(Figure figure)
    {
        figure.GetMoveTiles();

        isCheck = (figure is Pawn) 
            ? IsFigureCanAttackKing(figure.GetAttackTiles()) : IsFigureCanAttackKing(figure.GetSimulatedAttackTiles());        

        if (isCheck)
        {
            kingAttackedFigure = figure;

            checkedKing = (figure.team == Team.White)
                ? blackKing : whiteKing;

            checkedKing.GetTile().AttackMarkerSetActive(true);
            checkmate.IsCheckmate(figure, checkedKing);
        }
        else
        {
            if(checkedKing != null)
            {
                checkedKing.GetTile().AttackMarkerSetActive(false);
                checkedKing = null;
            }

            kingAttackedFigure = null;
        }
    }
    public void RemoveFigure(Figure figure)
    {
        if(figure.team == Team.White)
            whiteFigures.Remove(figure);
        else
            blackFigures.Remove(figure);
    }

    private bool IsFigureAttackKing(Team team, Figure attackedFigure)
    {
        if(team == Team.White)
        {
            foreach (Figure figure in blackFigures)
            {
                if (attackedFigure != null && figure == attackedFigure) continue;
               
                if(figure is not Pawn) figure.GetSimulatedMoveTiles();
                else figure.GetAttackTiles();
                List<Tile> path = figure.GetSimulatedAttackTiles();

                if (IsFigureCanAttackKing(path))
                    return true;
            }
            return false;
        }
        else
        {
            foreach (Figure figure in whiteFigures)
            {
                if (attackedFigure != null && figure == attackedFigure) continue;

                if (figure is not Pawn) figure.GetSimulatedMoveTiles();
                else figure.GetAttackTiles();

                List<Tile> path = figure.GetSimulatedAttackTiles();

                if (IsFigureCanAttackKing(path))
                    return true;
            }
            return false;
        }
    }

    private bool IsFigureCanAttackKing(List<Tile> path)
    {
        foreach (Tile attackTile in path)
            if (attackTile.figure is King)
                return true;
            
        return false;    
    }

    private void RemoveAllFigure()
    {
        foreach(Figure figure in whiteFigures)
            UnityEngine.Object.Destroy(figure.gameObject);

        foreach (Figure figure in blackFigures)
            UnityEngine.Object.Destroy(figure.gameObject);

        whiteFigures.Clear();
        blackFigures.Clear();
        isCheck = false;
    }
}
