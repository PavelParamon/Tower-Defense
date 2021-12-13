using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float x_min;
    [SerializeField] private float x_max;
    [SerializeField] private float z_min;
    [SerializeField] private float z_max;

    [SerializeField] private float _speedCamera;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (Input.GetKey(KeyCode.Mouse1))
            _camera.transform.position += new Vector3(-Input.GetAxis("Mouse X") * _speedCamera, 0,
                -Input.GetAxis("Mouse Y") * _speedCamera);

        _camera.transform.position = new Vector3(Mathf.Clamp(_camera.transform.position.x, x_min, x_max),
            _camera.transform.position.y, Mathf.Clamp(_camera.transform.position.z, z_min, z_max));
    }
}