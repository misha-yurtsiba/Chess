using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime
{
    private float gameTime;
    public float StartTime
    {
        get { return gameTime * 60; }
        set { gameTime = value; }
    }
}
