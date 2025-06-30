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
    private Collider2D _currentHit; // �߰�
    private float _holdTime;
    private float _holdThreshold = 1.0f;

    private InteractableObject _lastHighlighted;

    private void Update()
    {
        DetectInteractable();

        if (_currentInteractable != null)
        {
            _uiController.Show(_currentInteractable.GetDescription(), _currentHit.transform);
            // UI ����

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
            _uiController.Hide(); // UI �����
        }
    }

    private void DetectInteractable()
    {
        // ����
        _currentHit = Physics2D.OverlapCircle(transform.position, _interactRange, _interactableLayer);

        // ���� �� �� ��� ó��
        if (_currentHit == null)
        {
            Debug.Log("Hit ����");

            // ���� ���̶���Ʈ ����
            if (_lastHighlighted != null)
            {
                _lastHighlighted.SetHighlight(false);
                _lastHighlighted = null;
            }

            _currentInteractable = null;
            return;
        }

        //Debug.Log($"������: {_currentHit.name}");

        _currentInteractable = _currentHit.GetComponent<IInteractable>();

        // ���ο� ������ ������Ʈ�� ������ �ٸ��� ���̶���Ʈ ����
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
