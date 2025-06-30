using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField] private float _interactRange = 1.5f;
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private FarmingUIController _uiController;

    private IInteractable _currentInteractable;
    private Collider2D _currentHit; // 추가
    private float _holdTime;
    private float _holdThreshold = 1.0f;

    private InteractableObject _lastHighlighted;

    private void Update()
    {
        DetectInteractable();

        if (_currentInteractable != null)
        {
            _uiController.Show(_currentInteractable.GetDescription(), _currentHit.transform);
            // UI 띄우기

            if (Input.GetKey(KeyCode.E))
            {
                _holdTime += Time.deltaTime;
                if (_holdTime >= _holdThreshold)
                {
                    _currentInteractable.Interact();
                    _holdTime = 0f;
                }
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                _holdTime = 0f;
            }
        }
        else
        {
            _uiController.Hide(); // UI 숨기기
        }
    }

    private void DetectInteractable()
    {
        // 감지
        _currentHit = Physics2D.OverlapCircle(transform.position, _interactRange, _interactableLayer);

        // 감지 안 된 경우 처리
        if (_currentHit == null)
        {
            Debug.Log("Hit 없음");

            // 이전 하이라이트 꺼줌
            if (_lastHighlighted != null)
            {
                _lastHighlighted.SetHighlight(false);
                _lastHighlighted = null;
            }

            _currentInteractable = null;
            return;
        }

        //Debug.Log($"감지됨: {_currentHit.name}");

        _currentInteractable = _currentHit.GetComponent<IInteractable>();

        // 새로운 감지된 오브젝트가 이전과 다르면 하이라이트 갱신
        var currentHighlight = _currentHit.GetComponent<InteractableObject>();
        if (currentHighlight != null && currentHighlight != _lastHighlighted)
        {
            if (_lastHighlighted != null)
                _lastHighlighted.SetHighlight(false);

            currentHighlight.SetHighlight(true);
            _lastHighlighted = currentHighlight;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _interactRange);
    }

}
