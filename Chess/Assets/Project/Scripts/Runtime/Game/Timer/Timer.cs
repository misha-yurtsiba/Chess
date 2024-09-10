using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float startTime;

    public float curentTime;
    public float Seconds => Mathf.FloorToInt(curentTime % 60);
    public float Minutes => Mathf.FloorToInt(curentTime / 60);

    public Timer(float startTime)
    {
        this.startTime = startTime;

        curentTime = startTime;
    }
    public void Reset() => curentTime = startTime;
}
