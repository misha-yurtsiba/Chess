using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimeButtonItem : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI timeText { get; private set; }
    [field: SerializeField] public float time { get; private set; }
    public Button timeButton { get; private set; }

    public event Action<TimeButtonItem> onClick;

    public void Init()
    {
        timeButton = GetComponent<Button>();
        timeButton.onClick.AddListener(Click);
    }

    public void ChangeButtonColor(Color newColor)
    {
        timeButton.image.color = newColor;
    }

    private void Click() => onClick?.Invoke(this);
}
