using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private Rigidbody _rb;

    // Can change this variable to make the player move faster or slower
        private float _speed;
    public float _walkSpeed;
    public float _sprintSpeed;

    // Crouching functions
    public float _crouchSpeed;
    public float _crouchYScale;
    private float _startYScale;

    // This Variable make the player rotate at a set speed comment this out if the player should turn instantly    
    [SerializeField] private float _turnSpeed = 360;

    // Keybinds    
    public KeyCode _sprintKey = KeyCode.LeftShift;
    public KeyCode _crouchKey = KeyCode.LeftControl;


    private Vector3 _input;

    
    //Updated on start
    private void Start()
    {

        _startYScale = transform.localScale.y;
        _speed = _walkSpeed;
    }

    //Updating the functions listed bellow
    void Update()
    {

        GatherInput();
        Look();
        StateHandler();
       

    }

    //Updating the move function when the update is triggered
    void FixedUpdate()
    {

        Move();

    }

    //Tracking what the movement state is
    public MovementState state;

    public enum MovementState
    {

        walking,
        sprinting,
        crouching

    }

    private void StateHandler()
    {

        if (Input.GetKeyDown(_crouchKey))
        {
            state = MovementState.crouching;
            _speed = _crouchSpeed;

        }

        else if (Input.GetKeyUp(_crouchKey))
        {

            state = MovementState.walking;
            _speed = _walkSpeed;

        }

            //Mode sprinting
            if (Input.GetKeyDown(_sprintKey))
        {

            state = MovementState.sprinting;
            _speed = _sprintSpeed;

        }

        //Mode Walking
        else if (Input.GetKeyUp(_sprintKey))
        {
            state = MovementState.walking;
            _speed = _walkSpeed;

        }

    }

    //Getting the input for the horizontal movement
    void GatherInput()
    {
        
        _input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));

        //Start Crouch
        if (Input.GetKeyDown(_crouchKey))
        {

            transform.localScale = new Vector3(transform.localScale.x, _crouchYScale, transform.localScale.z);
            _rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        //Stop Crouch
        if (Input.GetKeyUp(_crouchKey))
        {

            transform.localScale = new Vector3(transform.localScale.x, _startYScale, transform.localScale.z);
            

        }
    }

    //Controlling where the player is getting the character to rotate to
    void Look()
    {
        if (_input != Vector3.zero)
        {

            

            var relative = (transform.position + _input.ToIso()) - transform.position;
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
