using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _smoothSpeed = 5f;

    [SerializeField] Vector2 _minCameraBoundary;
    [SerializeField] Vector2 _maxCameraBoundary;

    public Vector2 MaxCameraBoundary
    {
        get => _maxCameraBoundary;
        set => _maxCameraBoundary = value;
    }

    public Vector2 MinCameraBoundary
    {
        get => _minCameraBoundary;
        set => _minCameraBoundary = value;
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(_player.position.x, _player.position.y, this.transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, _minCameraBoundary.x, _maxCameraBoundary.x);
        targetPos.y = Mathf.Clamp(targetPos.y, _minCameraBoundary.y, _maxCameraBoundary.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, _smoothSpeed);
    }
}