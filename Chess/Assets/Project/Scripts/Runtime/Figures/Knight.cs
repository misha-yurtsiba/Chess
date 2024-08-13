using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Figure
{
    public override List<Tile> GetMoveTiles()
    {
        movingList.Clear();
        attackList.Clear();

        AddTile(xPos - 2, zPos + 1);
        AddTile(xPos - 1, zPos + 2);
        AddTile(xPos + 1, zPos + 2);
        AddTile(xPos + 2, zPos + 1);
        AddTile(xPos - 2, zPos - 1);
        AddTile(xPos - 1, zPos - 2);
        AddTile(xPos + 1, zPos - 2);
        AddTile(xPos + 2, zPos - 1);

        return movingList;
    }
}
