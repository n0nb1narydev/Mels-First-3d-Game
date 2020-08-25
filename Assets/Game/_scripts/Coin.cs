using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private AudioSource _coinPickupSound;
    private Player _player;

    private void Start() 
    {
        _coinPickupSound = GetComponent<AudioSource>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void OnTriggerStay(Collider other) 
    {
        if(other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            _player.hasCoin = true;
            _coinPickupSound.Play();
            Destroy(this.gameObject, 1f);
        }
    }
}
