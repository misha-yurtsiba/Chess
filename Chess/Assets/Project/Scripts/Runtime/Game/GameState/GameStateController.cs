using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameStateController : MonoBehaviour
{
    private InputHandler inputHandler;
    private Camera camera;
    private CheckController checkAndMateController;
    private Checkmate checkmate;
    private TimerController timerController;
    private IRestart restartGame;

    [HideInInspector] public GameStateBase curentGameState;
    [HideInInspector] public WaitPlayerInputState waitPlayerInputState;
    [HideInInspector] public PlayerLoseState playerLoseState;
    [HideInInspector] public FigureMoveState figureMoveState;

    [HideInInspector] public Tile activeTile;
    [HideInInspector] public Team curentMovingTeam;

    [Inject]
    private void Construct(InputHandler inputHandler, CheckController checkAndMateController, Checkmate checkmate, TimerController timerController, IRestart restartGame)
    {
        this.checkmate = checkmate;
        this.inputHandler = inputHandler;
        this.checkAndMateController = checkAndMateController;
        this.restartGame = restartGame;
        this.timerController = timerController;
    }
    void Start()
    {
        camera = Camera.main;

        playerLoseState = new PlayerLoseState();
        waitPlayerInputState = new WaitPlayerInputState(inputHandler,this,camera);
        figureMoveState = new FigureMoveState(inputHandler,this,camera, checkAndMateController);

        curentMovingTeam = Team.White;
        timerController.RestartTimers();

        ChangeState(waitPlayerInputState);
    }

    private void OnEnable()
    {
        checkmate.checkmate += Lose;
        timerController.timeOver += Lose;
        restartGame.restart += ResetState;
    }
    private void OnDisable()
    {
        checkmate.checkmate -= Lose;
        timerController.timeOver -= Lose;
        restartGame.restart -= ResetState;

    }
    void Update()
    {
        curentGameState.Update();
        timerController.UpdateTimer();
    }

    public void ChangeState(GameStateBase newState)
    {
        curentGameState?.Exit();
        curentGameState = newState;
        curentGameState.Enter();
        Debug.Log(curentGameState);
    }
    private void Lose(Team team) => ChangeState(playerLoseState);

    public void ResetState()
    {
        curentMovingTeam = Team.White;
        ChangeState(waitPlayerInputState);
    }

    public void ChangeTeam()
    {
        curentMovingTeam = (curentMovingTeam == Team.White) ? Team.Black : Team.White;
        timerController.ChangeTimer();
    }
}
