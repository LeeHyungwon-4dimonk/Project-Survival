using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class LHWMouseItemData : MonoBehaviour
{
    // Change this to getter setter occurs NullreferenceError.
    // Need to find accessibility restrict.
    [SerializeField] public Image ItemSprite;
    [SerializeField] public TMP_Text ItemCount;
    [SerializeField] public InventorySlots AssignedInventorySlot;

    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemCount.text = "";
    }

    /// <summary>
    /// Updates the mouse item slot.
    /// </summary>
    /// <param name="invSlot"></param>
    public void UpdateMouseSlot(InventorySlots invSlot)
    {
        AssignedInventorySlot.AssignItem(invSlot);
        ItemSprite.sprite = invSlot.Data.Icon;
        // if item is stackable, return stack size.
        // if not, don't print item amount.
        ItemCount.text = invSlot.StackSize > 1 ? invSlot.StackSize.ToString() : "";
        ItemSprite.color = Color.white;
    }

    private void Update()
    {
        // Mouse Control
        if(AssignedInventorySlot.Data != null)
        {
            transform.position = Mouse.current.position.ReadValue();

            if(Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                ClearSlot();
                // TODO : Drop the item on the ground.
            }
        }
    }

    /// <summary>
    /// Clears the slot.
    /// </summary>
    public void ClearSlot()
    {
        AssignedInventorySlot.ClearSlot();
        ItemCount.text = "";
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
    }

    /// <summary>
    /// Defines whether mouse is on the UI.
    /// </summary>
    /// <returns></returns>
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}