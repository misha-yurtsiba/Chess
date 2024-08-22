using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CheckAndMateController
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

    public void SetKing(King king)
    {
        if (king.team == Team.White)
            whiteKing = king;
        else
            blackKing = king;
    }
    public List<Tile> GetFigurePath(Figure movingFigure,Tile movingFigureCurentTile)
    {
        curentPath.Clear();
        movingFigureCurentTile.figure = null;
        List<Tile> figurePaht = movingFigure.GetMoveTiles();

        foreach (Tile tile in figurePaht)
        {
            tile.figure = movingFigure;

            if (!IsFigureAttackKing(movingFigure.team))
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

        if (isCheck)
        {
            foreach (Tile tile in figureAttackTiles)
                if (tile.figure == kingAttackedFigure)
                    curentAttackTiles.Add(tile);
        }
        else
            foreach (Tile tile in figureAttackTiles)
                if (tile.figure is not King)
                    curentAttackTiles.Add(tile);

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

    private bool IsFigureAttackKing(Team team)
    {
        if(team == Team.White)
        {
            foreach (Figure figure in blackFigures)
            {
                figure.GetMoveTiles();
                List<Tile> path = (figure is Pawn) 
                    ? figure.GetAttackTiles(): figure.GetSimulatedAttackTiles();

                if (IsFigureCanAttackKing(path))
                    return true;
            }
            return false;
        }
        else
        {
            foreach (Figure figure in whiteFigures)
            {
                figure.GetMoveTiles();
                List<Tile> path = (figure is Pawn)
                    ? figure.GetAttackTiles(): figure.GetSimulatedAttackTiles();

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
}
