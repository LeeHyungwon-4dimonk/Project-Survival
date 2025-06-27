using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _movement;
    private Vector2 _lastMoveDir = Vector2.down;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        Vector2 moveInput = _movement.MoveInput;
        bool isMoving = moveInput.sqrMagnitude > 0.01f;

        if (isMoving)
        {
            _lastMoveDir = moveInput;
        }

        _animator.SetFloat("MoveX", _lastMoveDir.x);
        _animator.SetFloat("MoveY", _lastMoveDir.y);
    }
}
