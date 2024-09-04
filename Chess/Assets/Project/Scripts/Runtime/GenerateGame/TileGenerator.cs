using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileGenerator :ITileGenerator, IDisposable
{
    private Tile tilePrefab;
    private Board board;
    private DiContainer diContainer;
    private IRestart restartGame;

    private float tileOffset;
    public TileGenerator(Tile tilePrefab, DiContainer diContainer, Board board, IRestart restartGame)
    {
        this.tilePrefab = tilePrefab;
        this.diContainer = diContainer;
        this.board = board;
        this.restartGame = restartGame;

        restartGame.restart += RestartTiles;
    }

    public void Dispose() => restartGame.restart -= RestartTiles;

    public void GenerateTiles()
    {
        tileOffset = board.tileOffset;

        for(int i = 0; i < Board.X_COUNT; i++)
        {
            for(int j =0; j < Board.Y_COUNT; j++)
            {
                board.board[i, j] = GenerateOneTile(i,j,tileOffset);
            }
        }
    }

    private Tile GenerateOneTile(int xPos,int zPos, float offset)
    {
        Tile tile = diContainer
            .InstantiatePrefab(tilePrefab,new Vector3(xPos, offset, zPos),Quaternion.identity,null)
            .GetComponent<Tile>();

        tile.Init(xPos,zPos);
        return tile;
    }

    private void RestartTiles()
    {
        for (int i = 0; i < Board.X_COUNT; i++)
        {
            for (int j = 0; j < Board.Y_COUNT; j++)
            {
                board.board[i, j].ResetValue();
            }
        }
    }
}
