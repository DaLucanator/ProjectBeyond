using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraFall : MonoBehaviour
{

    [SerializeField] private float fallForce, groundDrag, jumpDrag;
    private Rigidbody rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.drag = groundDrag;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < -0.1f) 
        {
            rb.drag = jumpDrag;
            Fall();
        }
        else
        {
            rb.drag = groundDrag;
        }
    }

    private void Fall()
    {
        rb.AddForce(Vector3.up * -fallForce, ForceMode.Impulse);
    }
}
