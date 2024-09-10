using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
public class TimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI whiteTimerText;
    [SerializeField] private TextMeshProUGUI blackTimerText;

    [SerializeField] private Color activeTimerColor;
    [SerializeField] private Color inactiveTimerColor;
    [SerializeField] private Color redTimerColor;

    private TextMeshProUGUI curentTimerText;
    private TimerController timerController;

    public const string TIME_FORMAT = "{0}:{1:00}";

    [Inject]
    private void Construct(TimerController timerController)
    {
        this.timerController = timerController;
    }

    private void Start()
    {
        curentTimerText = whiteTimerText;
    }
    private void OnEnable() 
    {
        timerController.onValueChanged += UpdateTimerText;
        timerController.teamChanget += ChangeTeam;
        timerController.restartTimers += SetStartTime;
    } 
    private void OnDisable()
    {
        timerController.onValueChanged -= UpdateTimerText;
        timerController.teamChanget -= ChangeTeam;
        timerController.restartTimers -= SetStartTime;

    }
    private void UpdateTimerText(Timer timer)
    {
        curentTimerText.text = string.Format(TIME_FORMAT, timer.Minutes, timer.Seconds);
        curentTimerText.color = (timer.Minutes == 0) ? redTimerColor : activeTimerColor;
    }

    private void ChangeTeam(Team newTeam,Timer timer)
    {
        curentTimerText.color = inactiveTimerColor;

        curentTimerText = (newTeam == Team.White) ? whiteTimerText : blackTimerText;
        Debug.Log("ChangeTeam");
    }

    private void SetStartTime(Timer timer)
    {
        curentTimerText = whiteTimerText;
        blackTimerText.color = inactiveTimerColor;
        whiteTimerText.text = string.Format(TIME_FORMAT, timer.Minutes, timer.Seconds);
        blackTimerText.text = string.Format(TIME_FORMAT, timer.Minutes, timer.Seconds);
        Debug.Log("ChangeText");

    }

}
