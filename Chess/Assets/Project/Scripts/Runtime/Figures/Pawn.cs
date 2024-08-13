using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Figure
{
    private bool isFirstMoving = true;

    private List<Tile> attackList = new List<Tile>();
    public override List<Tile> GetMoveTiles()
    {
        movingList.Clear();
        attackList.Clear();


        if (isFirstMoving)
        {
            if(team == Team.White)
            {
                for (int i = 1; zPos + i < 4; i++)
                {
                    if (CheckMovingAndAttackLimitation(xPos, zPos + i))
                    {
                        movingList.Add(gameBoard.board[xPos, zPos + i]);
                    }
                    else
                        break;
                }
            }
            else
            {
                for (int i = 1; zPos - i > 3; i++)
                {
                    if (CheckMovingAndAttackLimitation(xPos, zPos - i))
                    {
                        movingList.Add(gameBoard.board[xPos, zPos - i]);
                    }
                    else 
                        break;
                }
            }
        }
        else
        {
            if(team == Team.White)
                AddTile(xPos, zPos + 1);
            else
                AddTile(xPos, zPos - 1);
        }

        return movingList;
    }
    public override void MoveTo(int x, int z)
    {
        base.MoveTo(x, z);
        isFirstMoving = false;
    }
    private new bool CheckMovingAndAttackLimitation(int x, int z)
    {
        if (x < 0 || x >= Board.X_COUNT || z < 0 || z >= Board.Y_COUNT) return false;

        if (gameBoard.board[x, z].figure != null)
        {
            if (gameBoard.board[x, z].figure.team == team) return false;
            else return false;
        }

        return true;
    }
}
