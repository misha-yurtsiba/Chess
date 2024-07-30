using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private ISceneLoader sceneLoader;

    [Inject]
    public void Construct(ISceneLoader sceneLoader)
    {
        this.sceneLoader = sceneLoader;
    }
    void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;

        sceneLoader.LoadScene(SceneName.Menu.ToString());
    }
}
