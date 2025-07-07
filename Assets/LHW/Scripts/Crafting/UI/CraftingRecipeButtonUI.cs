using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingRecipeButtonUI : MonoBehaviour
{
    [SerializeField] CraftingRecipe _recipe;
    [SerializeField] Image _resultImage;
    [SerializeField] TMP_Text _itemNameText;

    private void Awake() => Init();

    private void Init()
    {
        _resultImage.sprite = _recipe.ResultItem.Icon;
        _itemNameText.text = _recipe.ResultItem.Name;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    /// <summary>
    /// OnClick Event. Send Scriptable Object recipe Data to CraftingController/
    /// </summary>
    public void OnClick()
    {
        GetComponentInParent<CraftingController>().SelectRecipe(_recipe);        
    }
}