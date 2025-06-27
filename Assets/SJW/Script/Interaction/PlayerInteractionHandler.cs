using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                _holdTime = 0f;
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
            Debug.Log("Hit ����");

            foreach (var obj in FindObjectsOfType<InteractableObjectAdapter>())
                obj.SetOutline(false);

            _currentInteractable = null;
            _nearestTransform = null;
            return;
        }

        var sortedHits = _currentHits
            .OrderBy(hit => Vector2.Distance(transform.position, hit.transform.position))
            .ToList();

        string names = string.Join(", ", sortedHits.Select(hit => hit.name));
        Debug.Log($"������: {names}");

        foreach (var obj in FindObjectsOfType<InteractableObjectAdapter>())
            obj.SetOutline(false);

        foreach (var hit in sortedHits)
        {
            var adapter = hit.GetComponent<InteractableObjectAdapter>();
            if (adapter != null)
                adapter.SetOutline(true);
        }

        var nearest = sortedHits[0];
        _currentInteractable = nearest.GetComponent<IInteractable>();
        _nearestTransform = nearest.transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _interactRange);
    }
}

