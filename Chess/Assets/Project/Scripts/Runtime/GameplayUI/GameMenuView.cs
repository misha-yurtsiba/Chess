using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuView : MonoBehaviour
{
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
        restartButton.onClick.AddListener(gameplayUIController.Restart);
        exitButton.onClick.AddListener(gameplayUIController.Exit);
    }

}
