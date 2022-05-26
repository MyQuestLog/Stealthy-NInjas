using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 4;
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

            var relative = (transform.position + _input) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = rot;
        }
    }

    //Controlling the player movement and telling it when to move and when not to move
    void Move()
    {
        _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) *_speed *Time.deltaTime);

    }
}
