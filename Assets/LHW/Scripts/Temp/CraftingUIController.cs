using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CraftingUIController : MonoBehaviour
{
    public Image CraftingPanel;

    // I've set esc key to exit temporary. Can change inactivate key.
    void Update()
    {
        if (CraftingPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            CraftingPanel.gameObject.SetActive(false);
    }
}
