using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RepairRecipeButtonUI : MonoBehaviour
{
    [SerializeField] RepairRecipeSO _recipe;
    [SerializeField] Image _image;
    [SerializeField] TMP_Text _text;

    [SerializeField] int _index;
    public int Index => _index;

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

    private void Update()
    {
        IsRepaired();
    }

    public void OnClick()
    {
        GetComponentInParent<RepairController>().SelectRecipe(_recipe, _index);
    }

    private void IsRepaired()
    {
        if (GameManager.Instance.GameData.RepairedTask[_index]) gameObject.GetComponent<Button>().interactable = false;
        // Maybe add check image change?
        else gameObject.GetComponent<Button>().interactable = true;
    }
}
