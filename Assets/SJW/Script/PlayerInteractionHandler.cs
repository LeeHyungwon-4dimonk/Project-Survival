using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField] private float _interactRange = 1.5f;
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private FarmingUIController _uiController;

    private IInteractable _currentInteractable;
    private Collider2D[] _currentHits;
    private float _holdTime;
    private float _holdThreshold = 1.0f;

    private InteractableObject _lastHighlighted;
    private Transform _nearestTransform;

    private void Update()
    {
        DetectInteractable();

        if (_currentInteractable != null)
        {
            _uiController.Show(_currentInteractable.GetDescription(), _nearestTransform);

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
            _uiController.Hide();
        }
    }

    private void DetectInteractable()
    {
        _currentHits = Physics2D.OverlapCircleAll(transform.position, _interactRange, _interactableLayer);

        if (_currentHits.Length == 0)
        {
            Debug.Log("Hit 없음");

            if (_lastHighlighted != null)
            {
                _lastHighlighted.SetHighlight(false);
                _lastHighlighted = null;
            }

            _currentInteractable = null;
            return;
        }

        // 거리순 정렬
        List<Collider2D> sortedHits = new List<Collider2D>(_currentHits);
        sortedHits.Sort((a, b) =>
            Vector2.Distance(transform.position, a.transform.position)
            .CompareTo(Vector2.Distance(transform.position, b.transform.position))
        );

        // 감지된 이름 출력
        string names = string.Join(", ", sortedHits.Select(hit => hit.name));
        Debug.Log($"감지됨: {names}");

        // 가장 가까운 오브젝트 선택
        Collider2D nearest = sortedHits[0];
        _currentInteractable = nearest.GetComponent<IInteractable>();
        _nearestTransform = nearest.transform;

        var currentHighlight = nearest.GetComponent<InteractableObject>();
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
