using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingController : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;

    private CraftingRecipe _currentRecipe;

    private void Update()
    {
        UIUpdate();
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
}
