using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private GameplayUIController gameplayUIController;
    public override void InstallBindings()
    {
        InputHandlerBind();
        GameplayUIControlerBind();
    }

    private void InputHandlerBind()
    {
        Container
            .Bind<InputHandler>()
            .FromInstance(inputHandler)
            .AsSingle()
            .NonLazy();
    }

    private void GameplayUIControlerBind()
    {
        Container
            .Bind<GameplayUIController>()
            .FromInstance(gameplayUIController)
            .AsSingle()
            .NonLazy();
    }

    
}
