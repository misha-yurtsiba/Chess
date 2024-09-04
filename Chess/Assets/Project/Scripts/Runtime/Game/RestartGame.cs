using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
public class RestartGame : IRestartGame
{
    public event Action restart;

    private IEntryPoint entryPoint;

    public void Init(IEntryPoint entryPoint)
    {
        this.entryPoint = entryPoint;
    }

    public void Restart() 
    {
        restart?.Invoke();
        entryPoint.StartGame();
    } 
}
