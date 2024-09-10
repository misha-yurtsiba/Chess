using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class SelectTimeView : MonoBehaviour
{
    public Button closeButton;

    [SerializeField] private Button startButton;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private ISceneLoader sceneLoader;
    private GameTime gameTime;
    private TimeButtonItem [] timeButtonItems;

    [Inject]
    private void Construct(ISceneLoader sceneLoader, GameTime gameTime)
    {
        this.sceneLoader = sceneLoader;
        this.gameTime = gameTime;
    }
    void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        timeButtonItems = GetComponentsInChildren<TimeButtonItem>();

        foreach (TimeButtonItem item in timeButtonItems)
        {
            item.Init();
            item.timeText.text = $"{item.time}:00"; 
        }

        timeButtonItems[0].ChangeButtonColor(activeColor);
        gameTime.StartTime = timeButtonItems[0].time;
    }

    private void OnEnable()
    {
        foreach (TimeButtonItem item in timeButtonItems)
            item.onClick += SelectTime;
    }
    private void OnDisable()
    {
        foreach (TimeButtonItem item in timeButtonItems)
            item.onClick -= SelectTime;
    }

    private void StartGame()
    {
        sceneLoader.LoadScene(SceneName.Gameplay.ToString());
    }

    private void SelectTime(TimeButtonItem timeButtonItem)
    {
        foreach(TimeButtonItem item in timeButtonItems)
        {
            if(timeButtonItem == item)
            {
                item.ChangeButtonColor(activeColor);
                gameTime.StartTime = timeButtonItem.time;
            }
            else
            {
                item.ChangeButtonColor(inactiveColor);
            }

        }
    }
}
