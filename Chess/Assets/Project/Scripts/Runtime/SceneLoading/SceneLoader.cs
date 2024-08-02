using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneLoader : ISceneLoader, IDisposable
{
    private string nextScene;

    private bool isLoading = false;

    private LoadingUi loadingUi;
    private AsyncOperation loadingAsyncOperation;

    public SceneLoader(LoadingUi loadingUi)
    {
        this.loadingUi = loadingUi;
        this.loadingUi.endActivePanel += StartLoad;
        this.loadingUi.endClosingPanel += EndLoadScene;
    }

    public void Dispose()
    {
        loadingUi.endActivePanel -= StartLoad;
        loadingUi.endClosingPanel -= EndLoadScene;
    }

    public void LoadScene(string sceneName)
    {
        nextScene = sceneName;

        loadingUi.gameObject.SetActive(true);
        loadingUi.StartLoadAnim();

    }
    private void EndLoadScene()
    {
        isLoading = false;
        loadingUi.gameObject.SetActive(false);
    }
    private async void StartLoad()
    {
        loadingAsyncOperation = SceneManager.LoadSceneAsync(nextScene);
        loadingAsyncOperation.allowSceneActivation = false;

        do
        {
            await Task.Delay(100);
            loadingUi.loadProgress = loadingAsyncOperation.progress;
            
        } while (loadingAsyncOperation.progress < 0.9f);

        await Task.Delay(1000);

        loadingAsyncOperation.allowSceneActivation = true;
        loadingUi.StartClosingAnim();
    }
}
