using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Figure
{

    public override List<Tile> GetMoveTiles()
    {
        movingList.Clear();
        attackList.Clear();

        for (int i = 1; xPos + i < Board.X_COUNT && zPos + i < Board.X_COUNT;i++)
        {
            if (CheckMovingAndAttackLimitation(xPos + i, zPos + i))
                movingList.Add(gameBoard.board[xPos + i, zPos + i]);
            else break;
        }
        for(int i = 1; xPos - i >=0 && zPos + i < Board.X_COUNT;i++)
        {
            if (CheckMovingAndAttackLimitation(xPos - i, zPos + i))
                movingList.Add(gameBoard.board[xPos - i, zPos + i]);
            else break;
        }

        for (int i = 1; xPos - i >= 0 && zPos - i >= 0; i++)
        {
            if (CheckMovingAndAttackLimitation(xPos - i, zPos - i))
                movingList.Add(gameBoard.board[xPos - i, zPos - i]);
            else break;
        }
        for (int i = 1; xPos + i < Board.X_COUNT && zPos - i >= 0; i++)
        {
            if (CheckMovingAndAttackLimitation(xPos + i, zPos - i))
                movingList.Add(gameBoard.board[xPos + i, zPos - i]);
            else break;
        }

        return movingList;
    }
}
