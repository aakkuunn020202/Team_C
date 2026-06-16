using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.5f;

    private CharacterController controller;
    private Transform cameraTransform;
    
    private Vector2 inputDirection;
    private Vector3 velocity;
    private bool isJumpPressed;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        
        // メインカメラのTransformを取得（カメラの向きを基準にするため）
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    // Input Systemからの入力を受け取るメソッド (SendMessageで呼ぶ場合)
    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        isJumpPressed = value.isPressed;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // 地面接地判定（CharacterControllerの機能）
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 完全に0にせず、少し下向きの力を残して接地を安定させる
        }

        // 1. カメラの向きを基準にした移動方向の計算
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Y軸（上下）の要素を無視して水平方向のベクトルにする
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // 入力値とカメラの向きを掛け合わせて移動方向を決定
        Vector3 moveDirection = (forward * inputDirection.y + right * inputDirection.x).normalized;

        // 2. 移動と回転の処理
        if (moveDirection.magnitude >= 0.1f)
        {
            // CharacterControllerによる移動
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            // 移動方向へ滑らかに回転
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 3. ジャンプ処理
        if (isJumpPressed && controller.isGrounded)
        {
            // 物理公式: v = sqrt(h * -2 * g)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumpPressed = false; // 連打・押しっぱなし防止
        }

        // 4. 重力の適用
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}