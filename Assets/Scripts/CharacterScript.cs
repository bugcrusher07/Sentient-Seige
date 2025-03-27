using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference fireAction;
    [SerializeField] private InputActionReference lookAction;

    [Header("Sensitivity")]
    [SerializeField] private float xSensivity = 30f;
    [SerializeField] private float ySensivity = 30f;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private GameObject crosshair;

    private Vector2 inputDirection;
    private Vector3 characterMoveDirection;
    private Vector2 lookDirection;

    private bool isFired;
    private bool isMoving;
    private bool isLooking;

    private float movingSpeed = 9f;
    private float xRotation = 0.0f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensivity;
        xRotation = Mathf.Clamp(xRotation, -30f, 30f);
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensivity);
    }

    void OnEnable()
    {
        moveAction.action.Enable();
        fireAction.action.Enable();
        lookAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
        fireAction.action.Disable();
        lookAction.action.Disable();
    }

    void CaptureInput()
    {
        inputDirection = moveAction.action.ReadValue<Vector2>();

        // Convert 2D input to 3D vector (X and Z, Y is 0)
        characterMoveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);

        // Normalize to prevent faster diagonal movement
        if (characterMoveDirection.magnitude > 1)
        {
            characterMoveDirection.Normalize();
        }

        isMoving = inputDirection != Vector2.zero;
        isFired = fireAction.action.ReadValue<float>() > 0;
        lookDirection = lookAction.action.ReadValue<Vector2>();
        isLooking = lookDirection != Vector2.zero;
    }

    void Update()
    {
        CaptureInput();
        // Handle animations
                if (characterAnimator != null)
        {
            // characterAnimator.SetBool("Run_guard_AR", isMovingAni);
            characterAnimator.SetBool("isShootingAni", isFired);
            characterAnimator.SetBool("walkAndShoot", isMoving && isFired);
            characterAnimator.SetBool("isIdleAni",!isMoving);
            // Optional: Blend tree for movement speed
            // characterAnimator.SetFloat("MoveSpeed", characterMoveDirection.magnitude);
            // if( isMoving == true){
            characterAnimator.SetBool("WalkingForward",inputDirection.y> 0.1f);
            characterAnimator.SetBool("WalkingBackward", inputDirection.y < -0.1f); // S key
            characterAnimator.SetBool("StrafingLeft", inputDirection.x < -0.1f); // A key
            characterAnimator.SetBool("StrafingRight", inputDirection.x > 0.1f);
            // }
        }

    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            // Transform the movement direction from local to world space based on character's rotation
            Vector3 worldDirection = transform.TransformDirection(characterMoveDirection);

            // Move in world space
            transform.position += worldDirection * movingSpeed * Time.fixedDeltaTime;
            Debug.Log("we moving bruh");
        }

        if (isFired)
        {
            Debug.Log("we firing bruh");
        }
    }

    void LateUpdate()
    {
        if (isLooking)
        {
            ProcessLook(lookDirection);
        }




    }
}
