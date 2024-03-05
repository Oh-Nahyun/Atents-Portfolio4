using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeTimeText : MonoBehaviour
{
    TextMeshProUGUI timeText;
    float maxLifeTime;

    private void Awake()
    {
        timeText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        maxLifeTime = player.maxLifeTime;

        player.onLifeTimeChange += OnLifeTimeChange;

        timeText.text = $"{maxLifeTime:f2} Sec";
    }

    private void OnLifeTimeChange(float ratio)
    {
        timeText.text = $"{(maxLifeTime * ratio):f2} Sec";
    }
}
