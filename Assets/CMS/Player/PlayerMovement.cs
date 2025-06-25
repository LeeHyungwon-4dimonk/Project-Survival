using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats _playerStats;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
        _moveInput.Normalize();

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Shift");
        }
        
    }

    private void FixedUpdate()
    {
        float speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = _playerStats.RunSpeed;
        }
        else
        {
            speed = _playerStats.MoveSpeed;
        }

        Debug.Log($"Speed: {speed}");

        _rb.MovePosition(_rb.position + _moveInput * speed * Time.fixedDeltaTime);
    }
}
