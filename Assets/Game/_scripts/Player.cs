using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public int currentAmmo;
    private int _maxAmmo = 50;
    private bool _isReloading = false;
    private UI_Manager _uiManager;
    [SerializeField]
    private Text _reloadText;
    [SerializeField]
    public bool hasCoin = false;
     
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>(); 
        _navMeshAgent = GetComponent<NavMeshAgent>();  
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentAmmo = _maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);

    }

    
    void Update()
    {
        //if left click cast ray from center point of main camera  
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    
        if(Input.GetMouseButton(0) && currentAmmo > 0 && _isReloading == false)
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
            currentAmmo --;
            _uiManager.UpdateAmmo(currentAmmo);

            if(_shootSound.isPlaying == false)
            {
            _shootSound.Play();
            }
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;

            if(currentAmmo == 0)
            {
                StartCoroutine(FlashingReloadText());
            }          

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
        currentAmmo = _maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
        _isReloading = false;
    }
    
    IEnumerator FlashingReloadText()
    {
        while(currentAmmo == 0)
        {
        _reloadText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _reloadText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);  
        }
    }
    
}
