using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float playerHeight = 2f;
 
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float movementMultiplier = 10f;
    [SerializeField] float airMultiplier = 0.4f; //Keep the vlaue less than 1 

    [Header("Jumping")]
    public float jumpForce = 5f;

    [Header("Falling")]
    public float fallForce = 5f;
    [SerializeField] bool isFalling;

    [Header("keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Drag")]
    public float groundDrag = 6f;
    public float JumpDrag = 2f;

    float rbDrag = 6f;

    [SerializeField] float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    public float groundDistance = 0.4f;

    [SerializeField] Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;
    private Animator animator;
    private GameObject mech;
    private int dir;

    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        mech = GameObject.FindGameObjectWithTag("Mech");

        GameEvents.current.SetHorizontalMovement += MyInput;
        GameEvents.current.Jump += Jump;
        dir = 1;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(rb.velocity.y < -0.1f) { isFalling = true; }
        if(isGrounded) { isFalling = false; }

        //print(isGrounded)

        ControlDrag();

       // if (Input.GetKeyDown(jumpKey) && isGrounded)
       // {
       //     Jump();
        //}

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void MyInput(float moveDir)
    {
        horizontalMovement = moveDir;

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

        if (horizontalMovement == 1)
        {
            if(dir != 1)
            {
                dir = 1;
                mech.transform.Rotate(0, 180, 0);
            }
        }
        if (horizontalMovement == -1)
        {
            if (dir != -1)
            {
                dir = -1;
                mech.transform.Rotate(0, 180, 0);
            }
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            animator.SetBool("Jump", true);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Fall()
    {
        rb.AddForce(transform.up * -fallForce, ForceMode.Impulse);
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = JumpDrag;
        }
    }


    private void FixedUpdate()
    {
        MovePlayer();

        if (isFalling)
        {
            Fall();
        }
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            animator.SetBool("Jump", false);
            if(moveDirection != Vector3.zero)
            {
                animator.SetBool("Walking", true);
            }
            else { animator.SetBool("Walking", false); }
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            animator.SetBool("Jump", false);
            if (moveDirection != Vector3.zero)
            {
                animator.SetBool("Walking", true);
            }
            else { animator.SetBool("Walking", false); }
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
            animator.SetBool("Walking", false);
        }

    }
}
