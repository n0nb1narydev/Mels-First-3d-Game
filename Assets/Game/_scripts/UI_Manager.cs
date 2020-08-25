using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private GameObject _coinImage;


    public void UpdateAmmo(int count)
    {
        _ammoText.gameObject.SetActive(true);
        _ammoText.text = "Ammo: " + count;
    }
    public void CollectedCoin()
    {
        _coinImage.SetActive(true);
    }
    public void RemoveCoin()
    {
        _coinImage.SetActive(false);
    }
}
