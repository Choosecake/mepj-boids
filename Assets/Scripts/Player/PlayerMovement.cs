using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 _targetVelocity;

    public float movementSpeed = 1000.0f;

    private float _lastDirection;
    private float _lookDirection;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CalculateInput();
        
        print(_targetVelocity);
        // set rotation
        switch (_targetVelocity.x)
        {
            case 1: _lookDirection = -90;
                break;
            case -1: _lookDirection = 90;
                break;
        }
        switch (_targetVelocity.y)
        {
            case 1: _lookDirection = 0;
                break;
            case -1: _lookDirection = 180;
                break;
        }

        if (_targetVelocity == Vector2.zero)
        {
            _lookDirection = _lastDirection;
        }

        _lastDirection = _lookDirection;
        var newRotation = new Vector3(0, 0, _lookDirection);
        transform.rotation = Quaternion.Euler(newRotation);
    }

    private void FixedUpdate()
    {
        Move(_targetVelocity);
    }

    private void CalculateInput()
    {
        _targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _targetVelocity.Normalize();
    }

    private void Move(Vector2 targetVelocity)
    {        
        // Set rigidbody velocity
        _rigidbody2D.velocity = (targetVelocity * movementSpeed) * Time.deltaTime; // Multiply the target by deltaTime to make movement speed consistent across different framerates
    }
}
