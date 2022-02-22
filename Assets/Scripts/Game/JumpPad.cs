using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : PoweredGadget
{
    private Rigidbody rb;
    [SerializeField] private float angle = 45f, force = 10f;
    [SerializeField] private Vector3 flingPos;

    protected override void Start()
    {
        gadgetName = "Jump Pad";
        assignTag(gadgetName);
        base.Start();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.ToString());
        if ((other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Player")) && other.gameObject.GetComponentInParent<Rigidbody>() != null && isPowered)
        {
            rb = other.gameObject.GetComponentInParent<Rigidbody>();
            Fling();
        }
    }

    private void Fling()
    {
        Debug.Log("Fling!");

        rb.velocity = Vector3.zero;
        rb.gameObject.transform.position = (transform.position + flingPos);

        float forceFloat = Mathf.Cos(angle * Mathf.PI / 180) * force;
        Vector3 forceVector = new Vector3(forceFloat, forceFloat, 0f);
        rb.AddForce(forceVector,ForceMode.Impulse);
    }
}