using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
    [SerializeField]
    float _sensitivity;

	void Start ()
    {
        
	}

	void Update ()
    {
        var prevAngles = transform.localEulerAngles;
        prevAngles.y += Input.GetAxis("Mouse X") * _sensitivity;
        transform.localEulerAngles = prevAngles;     
	}
}
