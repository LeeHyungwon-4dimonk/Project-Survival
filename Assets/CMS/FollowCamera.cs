using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10); // Z축만 이동 (2D 전용)
    [SerializeField] private float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (target != null)
        {
            // Z축 고정, X/Y 따라감
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, offset.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        }
    }
}