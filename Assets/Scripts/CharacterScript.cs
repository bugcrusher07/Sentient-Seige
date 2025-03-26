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

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Camera mainCamera;
    // [SerializeField] private CharacterController controller;

    private Vector2 inputDirection;
    private Vector3 characterMoveDirection;
    private Vector2 lookDirection;


    private bool isFired;
    private bool isMoving;
    private bool isLooking;

    private float movingSpeed= 0.3f;
    private float xRotation = 0.0f;


    public void  ProcessLook(Vector2 input){
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY*Time.deltaTime)*ySensivity;
        xRotation = Mathf.Clamp(xRotation,-80f,80f);
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        transform.Rotate(Vector3.up* (mouseX*Time.deltaTime)*xSensivity);
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

    void CaptureInput(){

        inputDirection = moveAction.action.ReadValue<Vector2>();
        characterMoveDirection= new Vector3(inputDirection.x,0f,inputDirection.y);
        if (inputDirection!= Vector2.zero){
            Debug.Log($"yeah we moving{characterMoveDirection} ");
            isMoving =true;
        } else{
            isMoving = false;
        }
        isFired = fireAction.action.ReadValue<float>() > 0;
        lookDirection = lookAction.action.ReadValue<Vector2>();
        if( lookDirection != Vector2.zero){
            isLooking = true;
        }else{
            isLooking = false;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CaptureInput();
    }

    void FixedUpdate()
    {
        if ( isMoving == true){
            transform.position += characterMoveDirection*movingSpeed;
            // mainCamera.transform.position += characterMoveDirection*movingSpeed;
            Debug.Log("character is moving");
        }
        if( isFired==true ){
            Debug.Log("we firing bruh");
        }

    }

    void LateUpdate()
    {
        if ( isLooking == true){
            ProcessLook(lookDirection);

        }
    }
}
