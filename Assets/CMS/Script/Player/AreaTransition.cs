using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransition : MonoBehaviour
{
    [SerializeField] private Vector2 _secondMaxCameraBoundary;
    [SerializeField] private Vector2 _secondMinCameraBoundary;
    [SerializeField] private Vector2 playerPosOffset;
    [SerializeField] private Transform _exitPos;

    private FollowCamera _followCamera;

    private void Awake()
    {
        _followCamera = Camera.main.GetComponent<FollowCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("Transition Triggered!");

        _followCamera.MinCameraBoundary = _secondMinCameraBoundary;
        _followCamera.MaxCameraBoundary = _secondMaxCameraBoundary;

        // 위치 강제 이동 (지연 처리)
        StartCoroutine(DelayedTeleport(collision.transform));
    }

    private IEnumerator DelayedTeleport(Transform player)
    {
        yield return new WaitForFixedUpdate();
        player.position = _exitPos.position + (Vector3)playerPosOffset;
    }
}
