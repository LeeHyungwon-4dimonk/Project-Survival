using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransition : MonoBehaviour
{
    [SerializeField] private Vector2 _secondMaxCameraBoundary;
    [SerializeField] private Vector2 _secondMinCameraBoundary;
    [SerializeField] private Vector2 playerPosOffset;
    [SerializeField] private Transform _exitPos;
    [SerializeField] private bool _isEnteringBase;
    [SerializeField] private AudioSource _portal;


    private FollowCamera _followCamera;

    private void Awake()
    {
        _followCamera = Camera.main.GetComponent<FollowCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Debug.Log("Transition Triggered!");

        if (_portal != null && !_portal.isPlaying)
            _portal.Play();

        _followCamera.MinCameraBoundary = _secondMinCameraBoundary;
        _followCamera.MaxCameraBoundary = _secondMaxCameraBoundary;

        if (_isEnteringBase)
            GameManager.Instance.DayNightManager.EnterBase();
        else
            GameManager.Instance.DayNightManager.ExitBase();

        StartCoroutine(DelayedTeleport(collision.transform));
    }

    private IEnumerator DelayedTeleport(Transform player)
    {
        yield return new WaitForFixedUpdate();
        player.position = _exitPos.position + (Vector3)playerPosOffset;
    }
}
