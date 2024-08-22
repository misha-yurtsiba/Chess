using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Figure
{
    public override List<Tile> GetMoveTiles()
    {
        movingList.Clear();
        attackList.Clear();

        AddTile(xPos + 1, zPos + 1);
        AddTile(xPos - 1, zPos + 1);
        AddTile(xPos - 1, zPos - 1);
        AddTile(xPos + 1, zPos - 1);
        AddTile(xPos, zPos - 1);
        AddTile(xPos, zPos + 1);
        AddTile(xPos + 1, zPos);
        AddTile(xPos - 1, zPos);

        return movingList;
    }

    public override void MoveTo(int x, int z)
    {
        gameBoard.board[xPos, zPos].AttackMarkerSetActive(false);
        base.MoveTo(x, z);
    }
}
