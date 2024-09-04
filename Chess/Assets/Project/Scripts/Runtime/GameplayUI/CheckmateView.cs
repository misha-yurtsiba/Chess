using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CheckmateView : MonoBehaviour
{
    [SerializeField] private Image winerTeamImage;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    private GameplayUIController gameplayUIController;

    [Inject]
    private void Construct(GameplayUIController gameplayUIController)
    {
       this.gameplayUIController = gameplayUIController;
    }
    void Start()
    {
        exitButton.onClick.AddListener(gameplayUIController.Exit);
        restartButton.onClick.AddListener(gameplayUIController.Restart);
    }

    public void ShowWiner(Team team)
    {
        winerTeamImage.color = (team == Team.White) ? Color.white : Color.black;
    }
}
