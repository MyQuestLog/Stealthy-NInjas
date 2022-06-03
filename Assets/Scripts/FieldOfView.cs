
// Zach Bombardieri 1126016

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldOfView : MonoBehaviour
{
    // Radius in the view can be seen
    public float radius;
    // Set the Radius to 360 degrees
    [Range(0, 360)]
    // The angle the view can be seen
    public float angle;


    // Reference to the object that the script will search for (in this case destroy)
    public GameObject PlayerRef; 

    // Create a layerMask for the object that will be searched for
    public LayerMask targetMask;

    public LayerMask obstructionMask;
    // A obstructionMask for anything that will be able to block the view to the targetMask

    // If the player is visable or not
    public bool canSeePlayer; 

    private void Start()
    {
        // Set the PlayerRef to the object with tag name "Player"
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        // Creating a delayed offset checker instead of checking everyframe to have the game run smoother
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        // Check delay by 0.1 seconds
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        // Loop the Coroutine unitl the parent object is destoryed or this is disabled
        while (true)
        {
            // Wait 0.1 seconds before calling the FieldOfViewCheck function
            yield return wait;
            FieldOfViewCheck();
            if (canSeePlayer == true)
            {
                // If the player is inside the FOV call the Kill function
                Kill();
            }
        }
    }

    private void FieldOfViewCheck()
    {
        // Creating a collider that checks for objects with the layer set to targetMask (target layer)
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        // Check if the rangeChecks found anything on the tagetMask layer
        if (rangeChecks.Length != 0)
        {
            // Only check the first range check because we only will have the player
            // however if there are more this can be increased
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // Check if the player is inside the angle
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                // Set the distanceToTarget of how far the target is away from the current FOV
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // Create a raycast from the centre of the parent object towards the player which is limited by
                // the FOV distance, and then stop the raycast if there are any obstructions on the obstructionMask layer
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    // Set the value of canSeePlayer to true
                    canSeePlayer = true;
                else
                    // Set the value of canSeePlayer to false
                    canSeePlayer = false;
            }
            // If the check fails the player is not in the FOV
            else
                // Set the value of canSeePlayer to false
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            // Set the value of canSeePlayer to false
            canSeePlayer = false;
    }

    public void Kill()
    {
        // Kill objects with the tag "Player"
        Destroy(GameObject.FindWithTag("Player"));
        //Uncomment below to reload the Scene instead of killing the player
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Set the value of canSeePlayer to false
        canSeePlayer = false;
    }
}
