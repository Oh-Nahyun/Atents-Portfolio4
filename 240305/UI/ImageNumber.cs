using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageNumber : MonoBehaviour
{
    public Sprite[] numberImages;

    Image[] digits;

    /// <summary>
    /// 목표
    /// </summary>
    int number = 0;

    public int Number
    {
        get => number;
        set
        {
            if (number != value)
            {
                number = value;
            }
        }
    }

    private void Awake()
    {
        digits = GetComponentsInChildren<Image>();
    }

    private void Start()
    {
        //int a = Number % 10;
        //int b = a % 10;
        //int c = b % 10;
        //int d = c % 10;
        //int e = d % 10;

        //if (e == 0)
        //{
        //    digits[4].disable();
        //}

        //for (int i = 0; i < digits.Length; i++)
        //{
        //    digits[i] = numberImages[Number % (10 ^ (i + 1))];
        //}
    }
}

/// 실습_240305
/// number는 5번째 자리까지 표현 가능 (max = 99999)
/// number에 값을 세팅하면 digits에 적절한 이미지가 선택된다.
/// 사용되지 않는 자리는 disable 처리
