using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RepairController : UIBase
{
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private Button _repairButton;

    [SerializeField] private Button[] _recipeButton;

    private RepairRecipeSO _currentRecipe;


    private void Awake() => Init();

    private void Init()
    {
        _titleText.text = "";
        _descriptionText.text = "";
        _energyText.text = "";
    }

    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        UIUpdate();
        RepairUIUpdate();
    }

    public void SelectRecipe(RepairRecipeSO recipe)
    {
        _currentRecipe = recipe;
    }

    private void UIUpdate()
    {
        if (_currentRecipe != null)
        {
            _titleText.text = _currentRecipe.ProductName;
            _descriptionText.text = _currentRecipe.ProductDescription;
            _energyText.text = $"필요 에너지 : {_currentRecipe.ProductEnergy.ToString()}";
        }
    }

    private void RepairUIUpdate()
    {
        if(HasEnoughEnergy()) _repairButton.interactable = true;
        else _repairButton.interactable = false;
    }

    public void RepairShip()
    {
        ConsumeEnergy();
        IsSolved();
        Init();
    }

    private void ConsumeEnergy()
    {
        // TODO : consume Energy
        Debug.Log("Repair Success");
    }

    private bool HasEnoughEnergy()
    {
        // TODO : if energy is enough
        return true;
        // TODO : if energy is not enough
        return false;
    }

    private bool IsSolved()
    {
        // TODO : Quest? Task? Complete 
        return true;

        return false;
    }

    public void GoBackToCraftUI()
    {
        GameManager.Instance.InGameUIManager.HideUI();
    }
}
