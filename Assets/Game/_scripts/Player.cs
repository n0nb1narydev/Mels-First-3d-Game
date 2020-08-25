﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    CharacterController _controller;
    [SerializeField]
    private float _speed = 3.5f;
    private float _gravity = 1f;
    private float _jumpHeight = 7f;
    private float _yVelocity;
    NavMeshAgent _navMeshAgent;
    [SerializeField]
    private GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarkerPrefab;
    private GameObject _hitClone;
    [SerializeField]
    private AudioSource _shootSound;
    [SerializeField]
    private AudioSource _reloadSound;
    [SerializeField]
    private int _currentAmmo;
    private int _maxAmmo = 50;
    private bool _isReloading = false;

     
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>(); 
        _navMeshAgent = GetComponent<NavMeshAgent>();  
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _currentAmmo = _maxAmmo;
    }

    
    void Update()
    {
        //if left click cast ray from center point of main camera  
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    
        if(Input.GetMouseButton(0) && _currentAmmo > 0)
        {
            FireGun();
        }
        else
        {
            _muzzleFlash.SetActive(false);
            _shootSound.Stop();
        }

        if(Input.GetKeyDown(KeyCode.R) && _isReloading == false)
        {
            _isReloading = true;
            StartCoroutine(Reload());
        }

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
        yield return new WaitForSeconds(1f);
        _navMeshAgent.enabled = true;

    }
    void FireGun()
    {   
            _muzzleFlash.SetActive(true);
            _currentAmmo --;

            if(_shootSound.isPlaying == false)
            {
            _shootSound.Play();
            }
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;
            

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("Hit: " + hitInfo.transform.name);
                _hitClone = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                Destroy(_hitClone, 1.0f);
            } 
    }
    IEnumerator Reload()
    {
       
        _reloadSound.Play();
        yield return new WaitForSeconds(1.5f);
        _currentAmmo = _maxAmmo;
        _isReloading = false;
    }
    
    
    
}
