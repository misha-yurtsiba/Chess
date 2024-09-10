using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerController :IDisposable
{
    private Timer whiteTimer;   
    private Timer blackTimer;
    private Timer curentTimer;
    private Checkmate checkmate;
    private GameTime gameTime;
    private IRestart restart;
    private Team curentTeam;

    private float startTime;
    private bool isTimeStop;

    public event Action<Team> timeOver;
    public event Action<Timer> onValueChanged;
    public event Action<Timer> restartTimers;
    public event Action<Team, Timer> teamChanget;
    public TimerController(IRestart restart, Checkmate checkmate, GameTime gameTime)
    {
        this.gameTime = gameTime;
        this.restart = restart;
        this.checkmate = checkmate;
        
        startTime = gameTime.StartTime;
        whiteTimer = new Timer(startTime);
        blackTimer = new Timer(startTime);

        restart.restart += RestartTimers;
        checkmate.checkmate += StopTimers;

        curentTeam = Team.White;
        curentTimer = whiteTimer;
        isTimeStop = false;
    }
    public void Dispose()
    {
        restart.restart -= RestartTimers;
        checkmate.checkmate -= StopTimers;
    }

    public void RestartTimers()
    {
        whiteTimer.Reset();
        blackTimer.Reset();

        isTimeStop = false;
        curentTeam = Team.White;
        restartTimers?.Invoke(curentTimer);
    }
    public void StopTimers(Team team) => isTimeStop = true;
    public void UpdateTimer()
    {
        if (isTimeStop) return;
        
        if(curentTimer.curentTime > 0)
        {
            onValueChanged?.Invoke(curentTimer);
            curentTimer.curentTime -= Time.deltaTime;
        }
        else
        {
            isTimeStop = true;
            ChangeTimer();
            timeOver?.Invoke(curentTeam);
        }
    }

    public void ChangeTimer()
    {
        curentTeam = (curentTeam == Team.White) ? Team.Black : Team.White;
        curentTimer = (curentTimer == whiteTimer) ? blackTimer : whiteTimer;
        teamChanget?.Invoke(curentTeam,curentTimer);
    }

    
}
