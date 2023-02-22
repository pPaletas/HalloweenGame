using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _walkAcceleration = 2f;
    [SerializeField] private float _dashCooldown = 2f;
    [SerializeField] private float _dashSpeedMultiplier = 5f;
    [SerializeField] private float _dashAccMultiplier = 5f;
    [SerializeField] private float _gravity = 10f;

    private CharacterController _cc;

    private Vector2 _inputAxis;
    private bool _dash = false;

    private float _currentDashCooldown = 0f;
    private Vector3 _targetDirection;
    private Vector3 _currentVelocity;

    private void ReadInput()
    {
        _inputAxis.x = Input.GetAxisRaw("Horizontal");
        _inputAxis.y = Input.GetAxisRaw("Vertical");
        _dash = Input.GetKeyDown(KeyCode.LeftShift);
    }

    private void Move()
    {
        _targetDirection.x = _inputAxis.x;
        _targetDirection.z = _inputAxis.y;

        _targetDirection = _targetDirection.normalized * _walkSpeed;

        float acceleration = _walkAcceleration;

        CheckDash(ref acceleration);

        _currentVelocity = Vector3.Lerp(_currentVelocity, _targetDirection, acceleration * Time.deltaTime);

        _cc.Move(_currentVelocity * Time.deltaTime);
    }

    private void CheckDash(ref float acceleration)
    {
        if (_dash && _currentDashCooldown <= 0f)
        {
            _targetDirection *= _dashSpeedMultiplier;
            acceleration *= _dashAccMultiplier;
            _currentDashCooldown = _dashCooldown;
        }
        else
        {
            _currentDashCooldown -= Time.deltaTime;
        }
    }

    private void Fall()
    {
        if (!_cc.isGrounded)
        {
            _cc.Move(Vector3.down * _gravity * Time.deltaTime);
        }
    }

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ReadInput();
        Move();
        Fall();
    }
}