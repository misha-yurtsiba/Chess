using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Figure
{
    private bool isFirstMoving = true;
    public override List<Tile> GetMoveTiles()
    {
        movingList.Clear();

        if (isFirstMoving)
        {
            if(team == Team.White)
            {
                for (int i = 1; zPos + i < 4; i++)
                {
                    if (CheckMovingLimitation(xPos, zPos + i))
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
                    if (CheckMovingLimitation(xPos, zPos - i))
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

    public override List<Tile> GetAttackTiles()
    {
        attackList.Clear();

        if (team == Team.White)
        {
            CheckAttackLimitation(xPos + 1, zPos + 1);
            CheckAttackLimitation(xPos - 1, zPos + 1);
        }
        else
        {
            CheckAttackLimitation(xPos + 1, zPos - 1);
            CheckAttackLimitation(xPos - 1, zPos - 1);
        }

        return attackList;
    }
    public override void MoveTo(int x, int z)
    {
        base.MoveTo(x, z);
        isFirstMoving = false;
    }
    private bool CheckMovingLimitation(int x, int z)
    {
        if (x < 0 || x >= Board.X_COUNT || z < 0 || z >= Board.Y_COUNT) return false;

        if (gameBoard.board[x, z].figure != null)
            return false;

        return true;
    }

    private bool CheckAttackLimitation(int x, int z)
    {
        if (x < 0 || x >= Board.X_COUNT || z < 0 || z >= Board.Y_COUNT) return false;
        if(gameBoard.board[x,z].figure != null && gameBoard.board[x, z].figure.team != team)
        {
            attackList.Add(gameBoard.board[x, z]);
            return true;
        }
        return false;
    }
}
