using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class CraftingController : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Button _craftButton;

    private CraftingRecipe _currentRecipe;

    private void Update()
    {
        UIUpdate();
        CraftingUIUpdate(AbleToMakeItem(RecipeRequireItemDictionary(_currentRecipe), InventoryItemDictionary()));
    }

    /// <summary>
    /// For OnClick Button Event. Get the current selected recipe.
    /// </summary>
    /// <param name="recipe"></param>
    public void SelectRecipe(CraftingRecipe recipe)
    {
        _currentRecipe = recipe;
    }

    /// <summary>
    /// Update Recipe Description based on currentRecipe.
    /// </summary>
    private void UIUpdate()
    {
        if(_currentRecipe != null)
        {
            _image.sprite = _currentRecipe.resultItem.Icon;
            _nameText.text = _currentRecipe.resultItem.Name;
            _descriptionText.text = _currentRecipe.resultItem.Description;
        }
    }

    private void CraftingUIUpdate(bool isCraftable)
    {
        if(isCraftable) _craftButton.interactable = true;
        else _craftButton.interactable = false;
    }

    /// <summary>
    /// Compare Dictionary of inventory item and Recipe require item.
    /// If there's enough materials for recipe, return true.
    /// </summary>
    /// <param name="reqItemsAndNum"></param>
    /// <param name="possessItemsAndNum"></param>
    /// <returns></returns>
    private bool AbleToMakeItem(Dictionary<int, int> reqItemsAndNum, Dictionary<int, int> possessItemsAndNum)
    {
        if(reqItemsAndNum == null || possessItemsAndNum == null) return false;

        foreach(var key in reqItemsAndNum.Keys)
        {
            if (!possessItemsAndNum.ContainsKey(key) || possessItemsAndNum[key] < reqItemsAndNum[key])
                return false;
        }
        return true;
    }

    /// <summary>
    /// Convert require items list as dictionary.
    /// key - item id / value - numbers needed to create recipe.
    /// </summary>
    /// <param name="recipe"></param>
    /// <returns></returns>
    private Dictionary<int, int> RecipeRequireItemDictionary(CraftingRecipe recipe)
    {
        if(recipe == null) return null;

        Dictionary<int, int> reqItemsAndNum = new Dictionary<int, int>();

        int reqItemListCount = recipe.reqItem.Count;

        for(int i = 0;  i < reqItemListCount; i++)
        {
            if(recipe.reqItem[i] == null) continue;

            int itemID = recipe.reqItem[i].ItemId;
            if (reqItemsAndNum.ContainsKey(itemID)) reqItemsAndNum[itemID] += 1;
            else reqItemsAndNum[itemID] = 1;
        }
        return reqItemsAndNum;
    }

    /// <summary>
    /// Convert inventory items quantity as dictionary.
    /// key - item id / value - numbers of item that the player possess.
    /// </summary>
    /// <returns></returns>
    private Dictionary<int, int> InventoryItemDictionary()
    {
        Dictionary<int, int> possessItemsAndNum = new Dictionary<int, int>();

        int inventorySlot = InventoryManager.Instance.InventoryCount;

        for(int i = 0; i < inventorySlot; i++)
        {
            ItemSO item = InventoryManager.Instance.ReadFromInventory(i, out int stack);

            if (item == null) continue;

            if(possessItemsAndNum.ContainsKey(item.ItemId)) possessItemsAndNum[item.ItemId] += stack;
            else possessItemsAndNum[item.ItemId] = stack;
        }
        return possessItemsAndNum;
    }
}
