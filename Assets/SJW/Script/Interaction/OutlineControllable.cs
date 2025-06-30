using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OutlineControllable : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetOutlineEnabled(bool enabled)
    {
        if (_renderer != null && _renderer.material != null)
        {
            GetComponent<Renderer>().material.SetFloat("_OutlineEnabled", enabled ? 1f : 0f);
        }
    }
}
