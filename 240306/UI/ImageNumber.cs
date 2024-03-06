using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageNumber : MonoBehaviour
{
    public Sprite[] numberImages;

    Image[] digits;

    /// <summary>
    /// 목표값
    /// </summary>
    int number = -1;

    /// <summary>
    /// 숫자를 확인하고 설정하는 프로퍼티
    /// </summary>
    public int Number
    {
        get => number;
        set
        {
            if (number != value)
            {
                number = Mathf.Min(value, 99999);                   // 최대 5자리로 숫자 설정

                int temp = number;                                  // 임시 변수에 number 복사

                for (int i = 0; i < digits.Length; i++)
                {
                    if (temp != 0 || i == 0)                        // temp가 0이 아니면 처리
                    {
                        int digit = temp % 10;                      // 1자리 숫자 추출하기
                        digits[i].sprite = numberImages[digit];     // 추출한 숫자에 맞게 이미지 선택
                        digits[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        digits[i].gameObject.SetActive(false);      // temp가 0이면 그 자리수는 안보이게 만들기 (1자리 제외)
                    }

                    temp /= 10;                                     // 1자리 수 제거하기
                }

                /// 실습_240306
                /// %10 연산으로 마지막 자리의 숫자 추출하기 -> 이미지 선택
                /// /10 연산으로 마지막 자리 제거하기
                /// /10 결과가 0이 될 때까지 반복

                // 내가 작성한 코드
                //for (int i = 0; i < digits.Length; i++)
                //{
                //    int num = (number / (10 ^ i)) % 10;

                //    if (num == 0)
                //    {
                //        break;
                //    }

                //    Sprite image = numberImages[num];
                //    digits[i].sprite = image;

                //    //Debug.Log($"{i}번째 수 : {num}");
                //}
            }
        }
    }

    private void Awake()
    {
        digits = GetComponentsInChildren<Image>();
    }
}

/// 실습_240305
/// number는 5번째 자리까지 표현 가능 (max = 99999)
/// number에 값을 세팅하면 digits에 적절한 이미지가 선택된다.
/// 사용되지 않는 자리는 disable 처리
