using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private InputHandler inputHandler;
    public override void InstallBindings()
    {
        InputHandlerBind();
    }

    private void InputHandlerBind()
    {
        Container
            .Bind<InputHandler>()
            .FromInstance(inputHandler)
            .AsSingle()
            .NonLazy();
    }
}
