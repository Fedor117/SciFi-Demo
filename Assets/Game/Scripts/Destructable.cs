using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField]
    GameObject _crateDestroyed;

    public void SwapToDestroyed()
    {
        Instantiate(_crateDestroyed, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
