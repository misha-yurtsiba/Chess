using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

public class CheckmateView : MonoBehaviour
{
    [SerializeField] private Image winerTeamImage;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI titleText;

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

    public void ShowWiner(Team team, string title)
    {
        winerTeamImage.color = (team == Team.White) ? Color.white : Color.black;
        titleText.text = title;
    }
}
