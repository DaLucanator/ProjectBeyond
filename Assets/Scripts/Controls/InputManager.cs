using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Controls controls;
    private bool canCheck;
    [SerializeField] private Vector2 startPos, currentPos;
    private float moveDir;
    private float lastMoveDir;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        controls.Touch.PrimaryContact.performed += ctx => StartTouchPrimary(ctx);
        controls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void Update()
    {
        if(canCheck)
        {
            currentPos = ScreenToWorld(Camera.main, controls.Touch.PrimaryPosition.ReadValue<Vector2>());
            if (currentPos.x > startPos.x) { moveDir = 1; }
            else if (currentPos.x < startPos.x) { moveDir = -1; ; }
            else { moveDir = 0; }
            if (moveDir != lastMoveDir)
            {
                lastMoveDir = moveDir;
                GameEvents.current.SetHorizontalMovementMethod(moveDir);
            }
        }
    }


    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        canCheck = true;
        startPos = ScreenToWorld(Camera.main, controls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        canCheck = false;
        GameEvents.current.SetHorizontalMovementMethod(0);
    }

    private Vector3 ScreenToWorld(Camera camera, Vector3 position )
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }

    public Vector2 PrimaryPosition()
    {
        return ScreenToWorld(Camera.main, controls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
