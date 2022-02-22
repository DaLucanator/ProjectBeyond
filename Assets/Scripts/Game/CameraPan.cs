using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Gadget entranceDoor;
    private Transform player;
    private int i;
    private bool doorOpen;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject.transform;
        i = 0;
        transform.position = waypoints[i].position;
    }

    private void Update()
    {
        if (waypoints != null)
        {
            if (i >= waypoints.Length) 
            { 
                transform.position = player.position + offset;
                if (!doorOpen)
                {
                    GameEvents.current.PowerOnMethod(entranceDoor.gameObject.GetInstanceID());
                    doorOpen = true;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[i].position, speed * Time.deltaTime);
                if (transform.position == waypoints[i].position) { i++; }
            }
        }
    }
}
