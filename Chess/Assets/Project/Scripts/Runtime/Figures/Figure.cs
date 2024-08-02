using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Figure : MonoBehaviour
{
    public Team team { get; private set; }

    public int xPos;
    public int zPos;

    protected Board gameBoard;
    public void Init(int x,int z, Board board)
    {
        xPos = x;
        zPos = z;
        gameBoard = board;
    }

    public abstract List<Tile> GetMoveTiles();
    public abstract List<Tile> GetAttackTiles();

    public virtual void MoveTo(int x, int z)
    {
        transform.position = new Vector3(x, 0, z);
    }

    public void Lose() { }
}
