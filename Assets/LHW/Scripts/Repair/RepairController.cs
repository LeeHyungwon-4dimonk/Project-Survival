using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RepairController : UIBase
{
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private Button _repairButton;
    [SerializeField] private Image _energyBarImage;
    [SerializeField] private TMP_Text _energyBarText;

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
        UpdateEnergyBarUI();
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

    private void UpdateEnergyBarUI()
    {
        _energyBarImage.fillAmount = (float)GameManager.Instance.GameData.Energy / GameManager.Instance.GameData.MaxEnergy;
        _energyBarText.text = $"에너지 : {GameManager.Instance.GameData.Energy}";
    }

    public void RepairShip()
    {
        ConsumeEnergy();
        IsSolved();
        Init();
    }

    private void ConsumeEnergy()
    {
        if (HasEnoughEnergy())
        {
            GameManager.Instance.GameData.DecreaseEnergy(_currentRecipe.ProductEnergy);
        }
    }

    private bool HasEnoughEnergy()
    {
        if (_currentRecipe == null) return false;
        else if (GameManager.Instance.GameData.Energy >= _currentRecipe.ProductEnergy) return true;
        else return false;
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
