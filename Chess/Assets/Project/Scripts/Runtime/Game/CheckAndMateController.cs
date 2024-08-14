using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAndMateController
{
    public List<Figure> whiteFigures = new List<Figure>();
    public List<Figure> blackFigures = new List<Figure>();
    private Board board;

    private List<Tile> curentPath = new List<Tile>();

    public CheckAndMateController(Board board)
    {
          this.board = board;
    }

    public List<Tile> GetFigurePath(Figure movingFigure,Tile movingFigureCurentTile, List<Tile> figurePaht)
    {
        curentPath.Clear();
        movingFigureCurentTile.figure = null;

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

    private bool IsFigureAttackKing(Team team)
    {
        if(team == Team.White)
        {
            foreach (Figure figure in blackFigures)
            {
                figure.GetMoveTiles();
                List<Tile> path = figure.GetAttackTiles();

                if (IsKingAttacked(path))
                    return true;
            }
            return false;
        }
        else
        {
            foreach (Figure figure in whiteFigures)
            {
                figure.GetMoveTiles();
                List<Tile> path = figure.GetAttackTiles();

                if (IsKingAttacked(path))
                    return true;
            }
            return false;
        }
        
    }

    private bool IsKingAttacked(List<Tile> path)
    {
        foreach (Tile attackTile in path)
            if (attackTile.figure is King)
                return true;
            
        return false;    
    }
}
