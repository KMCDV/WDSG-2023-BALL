using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 positionOffset = new(0, 2, -8);
    public float sensitivity = 2f;

    private float _angleX;
    private float _angleY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        _angleX += Input.GetAxis("Mouse X") * sensitivity;
        _angleY += Input.GetAxis("Mouse Y") * sensitivity;

        Quaternion rotation = Quaternion.Euler(-_angleY, _angleX, 0);
        Vector3 position = rotation * positionOffset + target.transform.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}
