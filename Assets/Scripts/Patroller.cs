using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    public Transform[] waypoints;
    // Will allow user to attach to any patroller to set speed differently each time if they want
    public int speed;

    private int waypointIndex;
    private float dist;

    // Start is called before the first frame update
    void Start()
    {
        // Finds the waypoints set by user
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        // Creates a distance between the points
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if(dist < 1f)
        {
            IncreaseIndex();
        }
        Patrol();
    }

    void Patrol()
    {
        // Moves to the points at the speed the user sets (there is a max speed before the patrol unit breaks, if the unit dissapears try to lower speed)
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        // Increases waypointIndex variable from 0
        waypointIndex++;
            if(waypointIndex >= waypoints.Length)
        {
            // Resets waypoint index
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
    }
}
