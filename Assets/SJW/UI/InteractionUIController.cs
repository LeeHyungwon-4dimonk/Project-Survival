using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionUIController : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _text;

    public void Show(string description, Transform target)
    {
        _panel.SetActive(true);
        _text.text = description;
        transform.position = target.position + Vector3.up * 1.2f;
    }


    public void Hide()
    {
        _panel.SetActive(false);
    }
}
