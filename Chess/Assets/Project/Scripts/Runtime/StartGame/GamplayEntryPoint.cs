using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GamplayEntryPoint : MonoBehaviour
{
    private Board board;
    private ITileGenerator tileGenerator;

    [Inject]
    private void Construct(Board board, ITileGenerator tileGenerator)
    {
        this.board = board;
        this.tileGenerator = tileGenerator;
    }
    void Start()
    {
        tileGenerator.GenerateTiles(board);
    }

    
}
