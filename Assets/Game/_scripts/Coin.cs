﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip _coinPickupSound;
    private Player _player;
    [SerializeField]
    private Text _pickupText;
    

    private void Start() 
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player" && _player.hasCoin == false)
        {
        _pickupText.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {
        _pickupText.gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other) 
    {
        if(other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
        _player.hasCoin = true;
        AudioSource.PlayClipAtPoint(_coinPickupSound, transform.position, 1f); //instantiate sound clip (sound, location, volume)
        UI_Manager uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        if(uiManager != null)
        {
            uiManager.CollectedCoin();
        }
        Destroy(this.gameObject);
        _pickupText.gameObject.SetActive(false);
        }
    }
}
