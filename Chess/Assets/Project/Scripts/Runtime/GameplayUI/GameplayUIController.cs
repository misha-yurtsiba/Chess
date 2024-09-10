using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] private CheckmateView checkmateView;
    [SerializeField] private GameMenuView gameMenuView;
    [SerializeField] private MenuButtonView menuButtonView;

    private Checkmate checkmate;
    private SceneLoader sceneLoader;
    private TimerController timerController;
    private IRestartGame restartGame;

    private const string CHECKMATE = "Checkmate";
    private const string TIME_OVER = "Time Over";

    private bool isMenuActive;
    [Inject]
    private void Construct(Checkmate checkmate, SceneLoader sceneLoader, TimerController timerController, IRestartGame restartGame)
    {
        this.checkmate = checkmate;
        this.sceneLoader = sceneLoader;
        this.restartGame = restartGame;
        this.timerController = timerController;
    }
    private void Start()
    {
        checkmateView.gameObject.SetActive(false);
        gameMenuView.gameObject.SetActive(false);

        isMenuActive = false;
    }
    private void OnEnable()
    {
        checkmate.checkmate += ActiveCheckmateView;
        timerController.timeOver += TimeOwer;
    }
    private void OnDisable()
    {
        checkmate.checkmate -= ActiveCheckmateView;
        timerController.timeOver -= TimeOwer;
    }
    public void ActiveCheckmateView(Team team)
    {
        checkmateView.gameObject.SetActive(true);
        checkmateView.ShowWiner(team, CHECKMATE);
    }

    public void MenuView()
    {
        isMenuActive = !isMenuActive;
        gameMenuView.gameObject.SetActive(isMenuActive);
        menuButtonView.ChangeButtonSprite(isMenuActive);
    }

    public void Exit()
    {
        sceneLoader.LoadScene(SceneName.Menu.ToString());
    }

    public void Restart()
    {
        checkmateView.gameObject.SetActive(false);
        gameMenuView.gameObject.SetActive(false);
        menuButtonView.ChangeButtonSprite(false);
        restartGame.Restart();
    }

    public void TimeOwer(Team team)
    {
        checkmateView.gameObject.SetActive(true);
        checkmateView.ShowWiner(team, TIME_OVER);
    }
}
