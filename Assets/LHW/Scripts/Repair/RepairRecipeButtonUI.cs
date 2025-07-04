using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RepairRecipeButtonUI : MonoBehaviour
{
    [SerializeField] RepairRecipeSO _recipe;
    [SerializeField] Image _image;
    [SerializeField] TMP_Text _text;

    private void Awake() => Init();

    private void Init()
    {
        _image.sprite = _recipe.Icon;
        _text.text = _recipe.ProductName;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        GetComponentInParent<RepairController>().SelectRecipe(_recipe);
    }
}
