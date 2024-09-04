using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class GamplayEntryPoint : MonoBehaviour , IEntryPoint
{
    private CheckController checkController;
    private Checkmate checkmate;
    private ITileGenerator tileGenerator;
    private IFigureGeneator figureGeneator;
    private RestartGame restart;

    [Inject]
    private void Construct(ITileGenerator tileGenerator, IFigureGeneator figureGeneator, CheckController checkController, Checkmate checkmate, RestartGame restart)
    {
        this.tileGenerator = tileGenerator;
        this.figureGeneator = figureGeneator;
        this.checkController = checkController;
        this.checkmate = checkmate;
        this.restart = restart;
    }
    void Start()
    {
        tileGenerator.GenerateTiles();
        checkmate.Init(checkController);
        restart.Init(this);
        
        StartGame();

    }

    public void StartGame()
    {
        figureGeneator.GenerateFigures();   
    }

}
