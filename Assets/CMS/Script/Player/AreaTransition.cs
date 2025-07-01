using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransition : MonoBehaviour
{
    private FollowCamera _followCamera;
    [SerializeField] Vector2 _secondMaxCameraBoundary;
    [SerializeField] Vector2 _secondMinCameraBoundary;
    [SerializeField] Vector2 playerPosOffset;
    [SerializeField] Transform _exitPos;

    private void Awake()
    {
        _followCamera = Camera.main.GetComponent<FollowCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _followCamera.MinCameraBoundary = _secondMinCameraBoundary;
            _followCamera.MaxCameraBoundary = _secondMaxCameraBoundary;
            collision.transform.position = _exitPos.position + (Vector3)playerPosOffset;
        }
    }

}
