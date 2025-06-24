using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField] private float _interactRange = 1.5f;
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private InteractionUIController _uiController;

    private IInteractable _currentInteractable;
    private Collider2D _currentHit; // Ãß°¡
    private float _holdTime;
    private float _holdThreshold = 1.0f;


    private void Update()
    {
        DetectInteractable();

        if (_currentInteractable != null)
        {
            _uiController.Show(_currentInteractable.GetDescription(), _currentHit.transform);
            // UI ¶ç¿ì±â

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
            _uiController.Hide(); // UI ¼û±â±â
        }
    }

    private void DetectInteractable()
    {
        _currentHit = Physics2D.OverlapCircle(transform.position, _interactRange, _interactableLayer);

        if (_currentHit == null)
        {
            _currentInteractable = null;
            return;
        }

        _currentInteractable = _currentHit.GetComponent<IInteractable>();
    }

}
