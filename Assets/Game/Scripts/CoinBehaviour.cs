using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField]
    AudioClip _pickUpSound;

    [SerializeField]
    float _soundVolume;

    void OnStart()
    {
        _pickUpSound = GetComponent<AudioClip>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other == null)
            return;

        if (Input.GetKeyDown(KeyCode.E) && other.tag == "Player")
        {
            var player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.PickUpCoin();
                AudioSource.PlayClipAtPoint(_pickUpSound, transform.position, _soundVolume);
                Destroy(gameObject);
            }
        }
    }
}
