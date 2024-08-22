using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameStateController : MonoBehaviour
{
    private InputHandler inputHandler;
    private Camera camera;
    private CheckAndMateController checkAndMateController;

    [HideInInspector] public GameStateBase curentGameState;
    [HideInInspector] public WaitPlayerInputState waitPlayerInputState;
    [HideInInspector] public FigureMoveState figureMoveState;

    [HideInInspector] public Tile activeTile;
    [HideInInspector] public Team curentMovingTeam;

    [Inject]
    private void Construct(InputHandler inputHandler, CheckAndMateController checkAndMateController)
    {
        this.inputHandler = inputHandler;
        this.checkAndMateController = checkAndMateController;
    }
    void Start()
    {
        camera = Camera.main;

        waitPlayerInputState = new WaitPlayerInputState(inputHandler,this,camera);
        figureMoveState = new FigureMoveState(inputHandler,this,camera, checkAndMateController);

        curentMovingTeam = Team.White;

        ChangeState(waitPlayerInputState);
    }

    
    void Update()
    {
        curentGameState.Update();
    }

    public void ChangeState(GameStateBase newState)
    {
        curentGameState?.Exit();
        curentGameState = newState;
        curentGameState.Enter();
        Debug.Log(curentGameState);
    }
}
