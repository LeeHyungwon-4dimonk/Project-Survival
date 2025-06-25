using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform lookTransform; // 라이트 또는 방향 기준이 되는 오브젝트

    private void Update()
    {
        RotateToMouse();
    }

    private void RotateToMouse()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector3 direction = (mouseWorldPosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        lookTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
