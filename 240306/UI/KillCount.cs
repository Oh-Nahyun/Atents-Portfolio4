using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    public float countingSpeed = 2.0f;

    float target = 0.0f;
    float current = 0.0f;


    ImageNumber imageNumber;

    private void Awake()
    {
        imageNumber = GetComponent<ImageNumber>();
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        player.onKillCountChange += OnKillCountChange;
    }

    private void Update()
    {
        current += Time.deltaTime * countingSpeed;  // current는 target까지 지속적으로 증가
        if (current > target)
        {
            current = target;                       // 넘치는 것 방지
        }
        imageNumber.Number = Mathf.FloorToInt(current);
    }

    private void OnKillCountChange(int count)
    {
        //imageNumber.Number = count;
        target = count;                             // 새 킬카운트를 target으로 지정
    }
}

/// 실습_240306
/// 플레이어가 잡은 적의 수 UI가 올라갈 때, 천천히 올라가도록 수정
