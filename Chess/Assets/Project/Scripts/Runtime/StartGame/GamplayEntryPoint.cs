using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GamplayEntryPoint : MonoBehaviour
{
    private Board board;
    private ITileGenerator tileGenerator;
    private IFigureGeneator figureGeneator;

    [Inject]
    private void Construct(Board board, ITileGenerator tileGenerator, IFigureGeneator figureGeneator)
    {
        this.board = board;
        this.tileGenerator = tileGenerator;
        this.figureGeneator = figureGeneator;
    }
    void Start()
    {
        tileGenerator.GenerateTiles(board);
        figureGeneator.GenerateFigures();
    }

    
}
