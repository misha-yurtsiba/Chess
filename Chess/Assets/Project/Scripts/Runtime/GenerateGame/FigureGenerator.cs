using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class FigureGenerator  : IFigureGeneator
{
    private FigureConfig figureConfig;
    private Board board;
    private DiContainer diContainer;

    private Dictionary<FigureType,FigureData> figureDict = new Dictionary<FigureType, FigureData>();
    public FigureGenerator(FigureConfig figureConfig, Board board, DiContainer diContainer)
    {
        this.figureConfig = figureConfig;
        this.board = board;
        this.diContainer = diContainer;
    }

    public void GenerateFigures()
    {
        foreach (FigureData figureData in figureConfig.data)
        {
            figureDict.Add(figureData.figureType, figureData);
        }

        for (int i = 0; i < Board.X_COUNT; i++)
        {
            CreateOneFigure(i, 1, FigureType.Pawn, Team.White);
            CreateOneFigure(i, 6, FigureType.Pawn, Team.Black);
        } 

        CreateOneFigure(0, 0, FigureType.Rook, Team.White);
        CreateOneFigure(1, 0, FigureType.Knight, Team.White);
        CreateOneFigure(2, 0, FigureType.Bishop, Team.White);
        CreateOneFigure(3, 0, FigureType.Queen, Team.White);
        CreateOneFigure(4, 0, FigureType.King, Team.White);
        CreateOneFigure(5, 0, FigureType.Bishop, Team.White);
        CreateOneFigure(6, 0, FigureType.Knight, Team.White);
        CreateOneFigure(7, 0, FigureType.Rook, Team.White);

        CreateOneFigure(0, 7, FigureType.Rook, Team.Black);
        CreateOneFigure(1, 7, FigureType.Knight, Team.Black);
        CreateOneFigure(2, 7, FigureType.Bishop, Team.Black);
        CreateOneFigure(3, 7, FigureType.Queen, Team.Black);
        CreateOneFigure(4, 7, FigureType.King, Team.Black);
        CreateOneFigure(5, 7, FigureType.Bishop, Team.Black);
        CreateOneFigure(6, 7, FigureType.Knight, Team.Black);
        CreateOneFigure(7, 7, FigureType.Rook, Team.Black);
    }

    private void CreateOneFigure(int xPos, int zPos,FigureType figureType,Team team)
    {
        Figure figure = diContainer.InstantiatePrefab
            (figureDict[figureType].figurePrefab,new Vector3(xPos,0,zPos),Quaternion.identity,null)
            .GetComponent<Figure>();

        if(team == Team.Black)
        {
            figure.GetComponent<MeshRenderer>().material = figureDict[figureType].darkMaterial;
            figure.transform.rotation = Quaternion.Euler(0,180,0);
        }
        else
        {
            figure.GetComponent<MeshRenderer>().material = figureDict[figureType].lightMaterial;

        }

        figure.Init(xPos, zPos, board, team);
        board.board[xPos, zPos].team = team;
        board.board[xPos, zPos].figure = figure;
    }
}
