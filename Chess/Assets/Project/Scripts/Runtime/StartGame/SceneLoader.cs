using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : ISceneLoader, ITickable, IDisposable
{
    private string nextScene;

    private bool isLoading = false;

    private LoadingUi loadingUi;
    private AsyncOperation loadingAsyncOperation;

    public SceneLoader(LoadingUi loadingUi)
    {
        this.loadingUi = loadingUi;
        this.loadingUi.endActivePanel += StartLoadScene;
        this.loadingUi.endClosingPanel += EndLoadScene;
    }

    public void Dispose()
    {
        loadingUi.endActivePanel -= StartLoadScene;
        loadingUi.endClosingPanel -= EndLoadScene;
    }

    public void LoadScene(string sceneName)
    {
        nextScene = sceneName;

        loadingUi.gameObject.SetActive(true);
        loadingUi.StartLoadAnim();

    }
    private void StartLoadScene()
    {
        isLoading = true;
        loadingAsyncOperation = SceneManager.LoadSceneAsync(nextScene);
    }
    private void EndLoadScene()
    {
        isLoading = false;
        loadingUi.gameObject.SetActive(false);
    }
    void ITickable.Tick()
    {
        if (!isLoading) return;

        if (loadingAsyncOperation.isDone)
            loadingUi.StartClosingAnim();

        if(loadingAsyncOperation != null)
            loadingUi.loadProgress = loadingAsyncOperation.progress;

    }
}
