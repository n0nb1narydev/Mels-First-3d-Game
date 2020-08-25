using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField]
    private GameObject _crateDestroyed;
    [SerializeField]
    private GameObject _cratePieces;

    public void DestroyCrate()
    {
        _cratePieces = Instantiate(_crateDestroyed, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(_cratePieces, 10.0f);
    }

}
