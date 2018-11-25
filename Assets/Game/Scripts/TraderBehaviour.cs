using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderBehaviour : MonoBehaviour
{
    [SerializeField]
    AudioClip _tradeSound;

    void OnTriggerStay(Collider other)
    {
        if (other == null)
            return;

        if (Input.GetKeyDown(KeyCode.E) && other.tag == "Player")
        {
            var player = other.GetComponent<PlayerController>();
            if (player != null && player.HasCoin)
            {
                player.GiveCoin();
                player.EnableWeapon();
                AudioSource.PlayClipAtPoint(_tradeSound, Camera.main.transform.position, .7f);
            }
        }
    }
}
