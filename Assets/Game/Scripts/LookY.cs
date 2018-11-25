using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    [SerializeField]
    float _sensitivity;

    void Start()
    {

    }

    void Update()
    {
        var prevAngles = transform.localEulerAngles;
        prevAngles.x -= Input.GetAxis("Mouse Y") * _sensitivity;

        if (prevAngles.x < 320 && prevAngles.x > 65)
        {
            if (prevAngles.x < 320 && prevAngles.x > 180)
                prevAngles.x = 320;
            else if (prevAngles.x > 65)
                prevAngles.x = 65;
        }

        transform.localEulerAngles = prevAngles;
    }
}

