using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController _controller;
    [SerializeField]
    private float _speed = 3.5f;
    private float _gravity = .7f;
    private float _jumpHeight = 12f;
    private float _yVelocity;

     
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");  
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0 , verticalInput);
        Vector3 velocity = _speed * direction;
        velocity.y = _yVelocity;
        _yVelocity -= _gravity; 
        
            //To Jump
        if(_controller.isGrounded == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
          
        _controller.Move(velocity * Time.deltaTime);
    }
}
