using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private ISceneLoader sceneLoader;

    [Inject]
    private void Construct(ISceneLoader sceneLoader)
    {
        this.sceneLoader = sceneLoader;
    }
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void StartGame()
    {
        sceneLoader.LoadScene(SceneName.Gameplay.ToString());
    }
    private void QuitGame()
    {
        Application.Quit();
    }
}
