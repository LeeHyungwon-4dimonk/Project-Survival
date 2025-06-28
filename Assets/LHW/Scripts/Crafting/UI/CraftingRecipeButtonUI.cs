using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingRecipeButtonUI : MonoBehaviour
{
    [SerializeField] CraftingRecipe _recipe;
    [SerializeField] Image _resultImage;
    [SerializeField] Image[] _requireImage;

    private void Awake() => Init();

    private void Init()
    {
        _resultImage.sprite = _recipe.resultItem.Icon;
        for(int i = 0; i < _recipe.reqItem.Count; i++)
        {
            _requireImage[i].sprite = _recipe.reqItem[i].Icon;
        }
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        GetComponentInParent<CraftingController>().SelectRecipe(_recipe);
        Debug.Log("РќДо");
    }
}
