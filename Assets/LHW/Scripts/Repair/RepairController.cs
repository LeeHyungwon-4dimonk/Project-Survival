using System.Runtime.CompilerServices;
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

    private int _index;

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

    public void SelectRecipe(RepairRecipeSO recipe, int index)
    {
        _currentRecipe = recipe;
        _index = index;
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
        SetSolved();
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
        if (_currentRecipe == null || IsSolved()) return false;
        else if (GameManager.Instance.GameData.Energy >= _currentRecipe.ProductEnergy) return true;
        else return false;
    }

    private bool IsSolved()
    {
        if (GameManager.Instance.GameData.RepairedTask[_index]) return true;
        else return false;
    }

    private void SetSolved()
    {
        GameManager.Instance.GameData.RepairComplete(_index);
    }

    public void GoBackToCraftUI()
    {
        InventoryManager.Instance.OpenRepairPanel();
    }
}
