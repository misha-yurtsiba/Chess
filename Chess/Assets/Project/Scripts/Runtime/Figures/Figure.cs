using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Figure : MonoBehaviour
{
    public Team team { get; private set; }

    public int xPos;
    public int zPos;

    protected Board gameBoard;

    protected List<Tile> movingList = new List<Tile>(); 
    protected List<Tile> attackList = new List<Tile>();
    protected List<Tile> attackSimulatedList = new List<Tile>();
    protected List<Tile> moveSimulatedList = new List<Tile>();

    public void Init(int x,int z, Board board, Team team)
    {
        xPos = x;
        zPos = z;
        gameBoard = board;
        this.team = team;
    }

    public abstract List<Tile> GetMoveTiles();
    public List<Tile> GetSimulatedMoveTiles()
    {
        moveSimulatedList.Clear();
        List<Tile> curentMoweTiles = movingList;
        GetMoveTiles();

        foreach (Tile tile in movingList)
            moveSimulatedList.Add(tile);

        movingList = curentMoweTiles;
        return attackSimulatedList;
    }
    public virtual List<Tile> GetAttackTiles()
    {
        GetMoveTiles();
        return attackList;
    }

    public List<Tile> GetSimulatedAttackTiles()
    {
        attackSimulatedList.Clear();
        List<Tile> curentAttackTiles = attackList;

        foreach (Tile tile in attackList)
            attackSimulatedList.Add(tile);

        attackList = curentAttackTiles;
        return attackSimulatedList;
    }
    public virtual void MoveTo(int x, int z)
    {
        transform.position = new Vector3(x, 0, z);
        xPos = x;
        zPos = z;
    }
    public Tile GetTile()
    {
        return gameBoard.board[xPos,zPos];
    }
    protected bool CheckMovingAndAttackLimitation(int x, int z)
    {
        if (x < 0 || x >= Board.X_COUNT || z < 0 || z >= Board.Y_COUNT) return false;

        if (gameBoard.board[x, z].figure != null)
        {
            if (gameBoard.board[x, z].figure.team == team) return false;
            else 
            {
                attackList.Add(gameBoard.board[x,z]);
                return false;
            } 
        }

        return true;
    }
    protected void AddTile(int x, int z)
    {
        if (CheckMovingAndAttackLimitation(x, z))
            movingList.Add(gameBoard.board[x, z]);
    }
    
    public void Lose() { }
}
