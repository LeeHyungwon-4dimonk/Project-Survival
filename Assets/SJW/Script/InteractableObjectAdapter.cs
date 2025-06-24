using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectAdapter : MonoBehaviour, IInteractable
{
    private InteractableObject _interactableObject;

    private void Awake()
    {
        _interactableObject = GetComponent<InteractableObject>();
    }

    public void Interact()
    {
        _interactableObject?.OnInteract();
    }

    public string GetDescription()
    {
        return "E - Ȯ���ϱ�"; // ���߿� Ȯ��
    }

    public KeyCode GetKey()
    {
        return KeyCode.E;
    }
}

