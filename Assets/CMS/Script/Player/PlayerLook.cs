using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform _lookTransform;       
    [SerializeField] private SpriteRenderer _playerRenderer;  

    private void Update()
    {
        RotateToMouse();
        FlipToMouse();
    }

    private void RotateToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector3 direction = (mouseWorldPos - _lookTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _lookTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void FlipToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mouseWorldPos - transform.position;

        _playerRenderer.flipX = direction.x < 0f;
    }
}
