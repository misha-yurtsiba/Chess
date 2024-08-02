using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class LoadingInstaler : MonoInstaller
{
    [SerializeField] private LoadingUi loadingUi;
    public override void InstallBindings()
    {
        BindLoadingUI();
        BindSceneLoadaig();
    }

    private void BindLoadingUI()
    {
        Container
            .Bind<LoadingUi>()
            .FromInstance(loadingUi)
            .AsSingle()
            .NonLazy();
    }

    private void BindSceneLoadaig()
    {
        Container
            .BindInterfacesAndSelfTo<SceneLoader>()
            .AsSingle()
            .NonLazy();
    }
}