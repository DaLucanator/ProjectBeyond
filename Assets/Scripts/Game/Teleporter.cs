using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Teleporter : PoweredGadget
{
    [SerializeField] private Transform otherTeleSpawn;
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private bool flipX, flipY;
    public GameObject gameobject;

    protected override void Start()
    {
        gadgetName = "Button";
        assignTag(gadgetName);
        base.Start();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = otherTeleSpawn.position;
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (flipX) { rb.velocity += new Vector3(rb.velocity.x * -2f, 0f, 0f); }
            if (flipY) { rb.velocity += new Vector3(0f, rb.velocity.y * -2f, 0f); }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, otherTeleSpawn.position);
    }
}
