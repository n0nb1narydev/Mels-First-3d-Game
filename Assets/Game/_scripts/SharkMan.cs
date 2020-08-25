using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharkMan : MonoBehaviour
{
 
    [SerializeField]
    private AudioClip _youWin;
    [SerializeField]
    private Text _interactText;

      private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
        _interactText.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {
        _interactText.gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other) 
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        UI_Manager uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        if(other.tag == "Player")
        {
           if(Input.GetKeyDown(KeyCode.E)) 
           {
               if(player.hasCoin == true)
               {
                    player.hasCoin = false;
                    uiManager.RemoveCoin();
                    player.EnableWeapons();
                    AudioSource.PlayClipAtPoint(_youWin, transform.position, 1f);

               } 
               else if (player.hasCoin = false)
               {
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.Play();
               }
           } 
         
        }    
    }
    //check for collision with player
    //if player 
    //check for ekey and if has coin
        //remove coin from player
        //update display
        //play win sound
    //if doesnt have coin grunt audio
}
