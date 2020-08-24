using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    CharacterController _controller;
    [SerializeField]
    private float _speed = 3.5f;
    private float _gravity = .7f;
    private float _jumpHeight = 10f;
    private float _yVelocity;
    NavMeshAgent _navMeshAgent;
    

     
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>(); 
        _navMeshAgent = GetComponent<NavMeshAgent>();   
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

        velocity = transform.transform.TransformDirection(velocity);
        
            //To Jump
        if(_controller.isGrounded == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(DisableAgent());
                _yVelocity = _jumpHeight;
                
            }
            
        }
          
        _controller.Move(velocity * Time.deltaTime);
    }
    //to allow jumping (try to find better way if possible)
    IEnumerator DisableAgent()
    {
        
        _navMeshAgent.enabled = false;
        yield return new WaitForSeconds(.7f);
        _navMeshAgent.enabled = true;

    }
}
