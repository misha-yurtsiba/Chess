using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileGenerator :ITileGenerator
{
    private Tile tilePrefab;
    private DiContainer diContainer;

    private float tileOffset;
    public TileGenerator(Tile tilePrefab, DiContainer diContainer)
    {
        this.tilePrefab = tilePrefab;
        this.diContainer = diContainer;
    }
    
    public void GenerateTiles(Board board)
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
        return tile;
    }
}
