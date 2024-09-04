using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Checkmate
{
    public event Action<Team> checkmate;

    private CheckController checkController;
    public void Init(CheckController checkController)
    {
        this.checkController = checkController;
    }

    public bool IsCheckmate(Figure attackFigure, King checkedKing)
    {
        if(checkController.GetFigurePath(checkedKing).Count != 0)
            return false;

        if(checkedKing.team == Team.White)
        {
            foreach (Figure figure in checkController.whiteFigures)
            {
                if (checkController.GetFigurePath(figure).Count != 0)
                    return false;
                if (checkController.GetAttackTiles(figure).Count != 0)
                    return false;
            }
        }
        else
        {
            foreach (Figure figure in checkController.blackFigures)
            {
                if (checkController.GetFigurePath(figure).Count != 0)
                    return false;
                if (checkController.GetAttackTiles(figure).Count != 0)
                    return false;
            }
        }


        Debug.Log("checkmate");
        checkmate?.Invoke(attackFigure.team);
        return true;
    }
}
