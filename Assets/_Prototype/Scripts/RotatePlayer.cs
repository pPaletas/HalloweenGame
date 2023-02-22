using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _mousePos;
    private Vector3 _mouseWorldPos;

    private void ReadInput()
    {
        _mousePos = Input.mousePosition;
        _mousePos.z = _camera.transform.position.y;
        _mouseWorldPos = _camera.ScreenToWorldPoint(_mousePos);
        Debug.Log(_mouseWorldPos);
        _mouseWorldPos.y = 0f;
    }

    private void Rotate()
    {
        Vector3 direction = (_mouseWorldPos - transform.position).normalized;
        direction.y = 0;
        direction.Normalize();
        transform.forward = direction;
    }

    private void Awake()
    {
        _camera = transform.root.GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        ReadInput();
        Rotate();
    }
}