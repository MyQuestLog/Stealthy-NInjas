using UnityEditor;
using UnityEngine;

//Whenever FieldOfView is active this will run
[CustomEditor(typeof(FieldOfView))] 
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        // Create a object set to target the editor
        FieldOfView fov = (FieldOfView)target;
        // Set the radius colour to white
        Handles.color = Color.white;
        // Create the Radius to be visable
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        // Create the left angle line
        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        // Create the right angle line
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        // Set the view angle lines to yellow
        Handles.color = Color.yellow;
        // Limits the length of the yellow line to the radius and makes the line visable for the left side
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
        // Limits the length of the yellow line to the radius and makes the line visable for the right side
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);

        // If the player is in view create a green line that bridges the player and enemy
        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.PlayerRef.transform.position);
        }
    }

    // Gets the viewing angle from the facing direction
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
