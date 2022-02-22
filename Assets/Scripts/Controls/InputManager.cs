using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    [SerializeField] private bool canCheck;
    [SerializeField] private Vector2 startPos, currentPos;
    [SerializeField] private float moveDir, lastMoveDir;
    [SerializeField] private float moveThreshold;
    private bool isMoving;
    private Animator animator;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        playerControls.Touch.PrimaryContact.performed += ctx => StartTouchPrimary(ctx);
        playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        playerControls.Touch.SecondaryContact.performed += ctx => StartTouchSecondary(ctx);
        playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        playerControls.Touch.JumpKeyboard.performed += ctx => Jump();
    }

    private void Update()
    {
        if(canCheck)
        {
            isMoving = true;
            currentPos = ScreenToWorld(Camera.main, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
            if (startPos.x - currentPos.x > moveThreshold || currentPos.x - startPos.x > moveThreshold)
            {
                if (currentPos.x > startPos.x) { moveDir = 1; }
                else if (currentPos.x < startPos.x) { moveDir = -1; ; }
                if (moveDir != lastMoveDir)
                {
                    lastMoveDir = moveDir;
                    GameEvents.current.SetHorizontalMovementMethod(moveDir);
                }
            }
            else 
            {
                moveDir = 0;
                GameEvents.current.SetHorizontalMovementMethod(0); 
            }
        }
    }

    private void Jump()
    {
        GameEvents.current.JumpMethod();
    }


    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        canCheck = true;
        if(!isMoving)
        {
            startPos = ScreenToWorld(Camera.main, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
        }
        if (context.interaction is UnityEngine.InputSystem.Interactions.MultiTapInteraction)
        {
            Jump();
            isMoving = false;
        }
    }


    private void StartTouchSecondary(InputAction.CallbackContext context)
    {
        canCheck = true;
        Debug.Log("2");
        if (!isMoving)
        {
            startPos = ScreenToWorld(Camera.main, playerControls.Touch.SecondaryPosition.ReadValue<Vector2>());
        }
        if (context.interaction is UnityEngine.InputSystem.Interactions.MultiTapInteraction)
        {
            Jump();
            isMoving = false;
        }
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
            canCheck = false;
            moveDir = 0;
            lastMoveDir = 0;
            GameEvents.current.SetHorizontalMovementMethod(0);
            isMoving = false;
    }

    private Vector3 ScreenToWorld(Camera camera, Vector3 position )
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }

    public Vector2 PrimaryPosition()
    {
        return ScreenToWorld(Camera.main, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
