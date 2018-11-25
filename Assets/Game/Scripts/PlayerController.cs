using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool HasCoin { get; set; }

    [SerializeField]
    float _speed;

    [SerializeField]
    GameObject _muzzleFlash;

    [SerializeField]
    GameObject _hitMarkerPrefab;

    [SerializeField]
    GameObject _weapon;

    [SerializeField]
    AudioSource _weaponAudio;

    [SerializeField]
    int _maxAmmo;

    [SerializeField]
    float _reloadTime;

    float _gravity = 9.81f;
    int _currentAmmo;
    bool _isReloading;
    CharacterController _controller;
    UiManager _uiManager;   

    public void PickUpCoin()
    {
        HasCoin = true;
        if (_uiManager != null)
            _uiManager.ShowCoin();
    }

    public void GiveCoin()
    {
        if (HasCoin)
        {
            HasCoin = false;

            if (_uiManager != null)
                _uiManager.HideCoin();
        }
    }

    public void EnableWeapon()
    {
        if (_weapon != null)
            _weapon.SetActive(true);
    }

	void Start ()
    {
        _controller = GetComponent<CharacterController>();
        _currentAmmo = _maxAmmo;

        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        UpdateAmmoOnUi();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.R) && (_currentAmmo < _maxAmmo) && !_isReloading)
        {
            StopShooting();
            StartCoroutine(ReloadWeapon());
        }

        if (Input.GetMouseButton(0) && (_currentAmmo > 0) && !_isReloading)
            StartShooting();
        else
            StopShooting();

        CalculateMovement();
	}

    void CalculateMovement()
    {
        var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        var velocity = direction * _speed;
        velocity.y -= _gravity;

        // Converting local world space value to global
        velocity = transform.TransformDirection(velocity);

        _controller.Move(velocity * Time.deltaTime);
    }

    void StartShooting()
    {
        if (_weapon == null || !_weapon.activeSelf)
            return;

        _currentAmmo--;

        if (_muzzleFlash != null)
            _muzzleFlash.SetActive(true);

        if ((_weaponAudio != null) && !_weaponAudio.isPlaying)
            _weaponAudio.Play();

        UpdateAmmoOnUi();

        // From the center of the screen
        var rayOrigin = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));

        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log(hitInfo.transform.name);
            StartCoroutine(InstantiateHitMarker(hitInfo));
        }
    }

    void StopShooting()
    {
        if (_muzzleFlash != null)
            _muzzleFlash.SetActive(false);

        if (_weaponAudio != null && _weaponAudio.isPlaying)
            _weaponAudio.Stop();
    }

    void UpdateAmmoOnUi()
    {
        if (_uiManager != null)
            _uiManager.UpdateAmmo(_currentAmmo);
    }

    IEnumerator ReloadWeapon()
    {
        _currentAmmo = 0;
        UpdateAmmoOnUi();

        _isReloading = true;
        yield return new WaitForSeconds(_reloadTime);
        _isReloading = false;
        _currentAmmo = _maxAmmo;

        UpdateAmmoOnUi();
    }

    IEnumerator InstantiateHitMarker(RaycastHit hitInfo)
    {
        var hitMarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        yield return new WaitForSeconds(.5f);
        Destroy(hitMarker);

        var crate = hitInfo.transform.GetComponent<Destructable>();
        if (crate != null)
            crate.SwapToDestroyed();
    }
}
