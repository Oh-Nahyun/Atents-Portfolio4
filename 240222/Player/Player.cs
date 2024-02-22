using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float speed = 3.0f;

    /// <summary>
    /// 현재 이동 속도
    /// </summary>
    float currentSpeed = 3.0f;

    /// <summary>
    /// 현재 입력된 이동 방향
    /// </summary>
    Vector2 inputDirection = Vector2.zero;

    /// <summary>
    /// 지금 움직이고 있는지 확인하는 변수 (true면 움직인다.)
    /// </summary>
    bool isMove = false;

    /// <summary>
    /// 공격 쿨타임
    /// </summary>
    public float attackCoolTime = 1.0f;

    /// <summary>
    /// 현재 남아있는 공격 쿨타임
    /// </summary>
    float currentAttackCoolTime = 0.0f;

    /// <summary>
    /// 공격 쿨타임이 다 되었는지 확인하기 위한 프로퍼티
    /// </summary>
    bool IsAttackReady => currentAttackCoolTime < 0.0f;

    /// <summary>
    /// AttackSecsor의 회전축
    /// </summary>
    Transform attackSensorAxis;

    // 컴포넌트들
    Rigidbody2D rigid;
    Animator animator;

    // 인풋액션
    PlayerInputActions inputActions;

    // 애니메이터용 해시값들
    readonly int InputX_Hash = Animator.StringToHash("InputX");
    readonly int InputY_Hash = Animator.StringToHash("InputY");
    readonly int IsMove_Hash = Animator.StringToHash("IsMove");
    readonly int Attack_Hash = Animator.StringToHash("Attack");

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputActions = new PlayerInputActions();

        currentSpeed = speed;

        attackSensorAxis = transform.GetChild(0);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnStop;
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Move.canceled -= OnStop;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        currentAttackCoolTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // 물리 프레임마다 inputDirection 방향으로 초당 currentSpeed만큼 이동
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * currentSpeed * inputDirection);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // 입력값 받아와서
        inputDirection = context.ReadValue<Vector2>();

        // 애니메이션 조정
        animator.SetFloat(InputX_Hash, inputDirection.x);
        animator.SetFloat(InputY_Hash, inputDirection.y);
        isMove = true;
        animator.SetBool(IsMove_Hash, isMove);

        // 공격 범위 회전시키기
        AttackSensorRotate(inputDirection);
    }

    private void OnStop(InputAction.CallbackContext _)
    {
        // 이동 방향을 0으로 만들고
        inputDirection = Vector2.zero;

        // InputX와 InputY를 변경하지 않는 이유
        // Idle 애니메이션을 마지막 이동 방향으로 재생하기 위해

        isMove = false; // 정지
        animator.SetBool(IsMove_Hash, isMove);
    }

    private void OnAttack(InputAction.CallbackContext _)
    {
        if (IsAttackReady) // 공격 쿨타임이 다 되었으면
        {
            animator.SetTrigger(Attack_Hash); // 애니메이션 재생
            currentAttackCoolTime = attackCoolTime; // 쿨타임 초기화
            currentSpeed = 0.0f; // 이동 정지
        }
    }

    /// <summary>
    /// 이동 속도를 원래대로 되돌리는 함수
    /// </summary>
    public void RestoreSpeed()
    {
        currentSpeed = speed;
    }

    /// <summary>
    /// 입력 방향에 따라 AttackSensor를 회전 시키는 함수
    /// </summary>
    /// <param name="direction">입력 방향</param>
    void AttackSensorRotate(Vector2 direction)
    {
        // 대각선은 위아래를 우선(시 한다.)
        if (direction.y < 0)
        {
            attackSensorAxis.rotation = Quaternion.identity; // 아래
        }
        else if (direction.y > 0)
        {
            attackSensorAxis.rotation = Quaternion.Euler(0, 0, 180); // 위
        }
        else if (direction.x < 0)
        {
            attackSensorAxis.rotation = Quaternion.Euler(0, 0, -90); // 왼쪽
        }
        else if (direction.x > 0)
        {
            attackSensorAxis.rotation = Quaternion.Euler(0, 0, 90); // 오른쪽
        }
        else
        {
            attackSensorAxis.rotation = Quaternion.identity; // 입력이 없음 (0, 0)
        }
    }
}

/// 실습_240219
/// 1. 입력처리
///     1.1. Player Input Action 맵 만들기
/// 2. Move 애니메이션 만들기
///     2.1. 10 프레임 간격으로 0~3번
/// 3. Move Blend Tree 만들기

/// 실습_240220
/// 플레이어가 공격하기
/// - 애니메이션만 재생
/// - 공격은 쿨타임이 있다. (1초)
/// - 이동 중 공격을 하면 공격 애니메이션 중에는 멈추고 애니메이션이 끝나면 다시 이동한다.
