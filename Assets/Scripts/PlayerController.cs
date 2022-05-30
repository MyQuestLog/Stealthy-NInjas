using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    // Can change this variable to make the player move faster or slower
    [SerializeField] private float _speed = 5;
    // This Variable make the player rotate at a set speed comment this out if the player should turn instantly
    [SerializeField] private float _turnSpeed = 360; 

    private Vector3 _input;

    //Updating the functions listed bellow
    void Update()
    {

        GatherInput();
        Look();
    }

    //Updating the move function when the update is triggered
    void FixedUpdate()
    {

        Move();

    }

    //Getting the input for the horizontal movement
    void GatherInput()
    {
        
        _input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));

    }

    //Controlling where the player is getting the character to rotate to
    void Look()
    {
        if (_input != Vector3.zero)
        {

            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0));

            var skewedInput = matrix.MultiplyPoint3x4(_input);

            var relative = (transform.position + skewedInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            //This code allows the implimentation of contoller support and also make the player rotate overtime 
            //however if the player should turn instantaniously comment the code bellow out and remove the comment on transform.rotation = rot;
            transform.rotation = Quaternion.RotateTowards(transform.rotation,rot, _turnSpeed * Time.deltaTime);
            //transform.rotation = rot;
        }
    }

    //Controlling the player movement and telling it when to move and when not to move
    void Move()
    {
        _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) *_speed *Time.deltaTime);

    }
}
