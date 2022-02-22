using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : PoweredGadget
{
    [SerializeField] private Vector3 moveOffset;
    [SerializeField] private float openSpeed;
    private Vector3 moveTo, initialPos;

    protected override void Start()
    {
        initialPos = transform.position;
        base.Start();
    }

    private void Update()
    {
        if (isPowered)
        {
            moveTo = initialPos + moveOffset;
        }
        if (!isPowered)
        {
            moveTo = initialPos;
        }
        if (transform.position != moveTo) { transform.position = Vector3.MoveTowards(transform.position, moveTo, openSpeed * Time.deltaTime); }
    }
}
