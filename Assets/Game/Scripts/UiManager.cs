using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class UiManager : MonoBehaviour
{
    [SerializeField]
    Text _ammoText;

    [SerializeField]
    GameObject _coin;

    public void UpdateAmmo(int count)
    {
        if (_ammoText != null)
            _ammoText.text = "Ammo: " + count;
    }

    public void ShowCoin()
    {
        if (_coin != null)
            _coin.SetActive(true);
    }

    public void HideCoin()
    {
        if (_coin != null)
            _coin.SetActive(false);
    }
}
