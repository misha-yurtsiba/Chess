using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private SelectTimeView selectTimeView;

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
        selectTimeView.closeButton.onClick.AddListener(() => selectTimeView.gameObject.SetActive(false));
    }

    private void StartGame()
    {
        selectTimeView.gameObject.SetActive(true);
    }
    private void QuitGame()
    {
        Application.Quit();
    }
}
