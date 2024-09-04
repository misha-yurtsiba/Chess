using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] private CheckmateView checkmateView;
    [SerializeField] private GameMenuView gameMenuView;

    private Checkmate checkmate;
    private SceneLoader sceneLoader;
    private IRestartGame restartGame;

    [Inject]
    private void Construct(Checkmate checkmate, SceneLoader sceneLoader, IRestartGame restartGame)
    {
        this.checkmate = checkmate;
        this.sceneLoader = sceneLoader;
        this.restartGame = restartGame;
    }
    private void Start()
    {
        checkmateView.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        checkmate.checkmate += ActiveCheckmateView;
    }
    private void OnDisable()
    {
        checkmate.checkmate -= ActiveCheckmateView;
    }
    public void ActiveCheckmateView(Team team)
    {
        checkmateView.gameObject.SetActive(true);
        checkmateView.ShowWiner(team);
    }

    public void ActiveMenuView()
    {
        gameMenuView.gameObject.SetActive(true);
    }

    public void Exit()
    {
        sceneLoader.LoadScene(SceneName.Menu.ToString());
    }

    public void Restart()
    {
        checkmateView.gameObject.SetActive(false);
        restartGame.Restart();
    }
}
