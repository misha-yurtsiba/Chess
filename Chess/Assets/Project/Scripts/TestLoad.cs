using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestLoad : MonoBehaviour
{
    private ISceneLoader sceneLoader;

    [SerializeField] private Button button;

    [Inject]
    public void Construct(ISceneLoader sceneLoader)
    {
        this.sceneLoader = sceneLoader;
    }
   
    void Start()
    {
        button.onClick.AddListener(Click);
    }

    public void Click()
    {
        sceneLoader.LoadScene(SceneName.Gameplay.ToString());
    }

}
