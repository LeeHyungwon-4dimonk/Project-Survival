using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingController : UIBase
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _energyRequireText;
    [SerializeField] private Button _craftButton;
    [SerializeField] private Image _sliderImage;
    [SerializeField] private Button _resultButton;
    [SerializeField] private Image _resultImage;
    [SerializeField] private Image _energyBarImage;
    [SerializeField] private TMP_Text _energyBarText;
    [SerializeField] private GameObject _restrictPanel;

    private CraftingRecipe _currentRecipe;

    //private Coroutine _craftCoroutine;

    private void Awake()
    {
        _restrictPanel.SetActive(false);
    }

    private void Update()
    {
        UIUpdate();
        CraftingUIUpdate();
        UpdateEnergyBarUI();
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
        if (_currentRecipe != null)
        {
            _image.color = Color.white;
            _image.sprite = _currentRecipe.ResultItem.Icon;
            _nameText.text = _currentRecipe.ResultItem.Name;
            _descriptionText.text = _currentRecipe.ResultItem.Description;
            _energyRequireText.text = $"요구 에너지 : {_currentRecipe.ProductEnergy.ToString()}";
        }
        else
        {
            _image.color = Color.clear;
            _image.sprite = null;
            _nameText.text = "";
            _descriptionText.text = "";
            _energyRequireText.text = "";
        }
    }

    /// <summary>
    /// Update Create Button.
    /// If item is Craftable, interaction will be occur.
    /// </summary>
    private void CraftingUIUpdate()
    {
        if (HasEnoughEnergy())
            _craftButton.interactable = true;
        else
            _craftButton.interactable = false;
    }

    private void UpdateEnergyBarUI()
    {
        _energyBarImage.fillAmount = (float)GameManager.Instance.GameData.Energy / GameManager.Instance.GameData.MaxEnergy;
        _energyBarText.text = $"에너지 : {GameManager.Instance.GameData.Energy}";
    }

    /// <summary>
    /// For OnClick button event. Craft Item.
    /// </summary>
    public void CraftItem()
    {
        ConsumeEnergy();
        ResultPrint();
    }

    /// <summary>
    /// If recipe is craftable, consume material item.
    /// </summary>
    private void ConsumeEnergy()
    {
        if (HasEnoughEnergy())
        {
            GameManager.Instance.GameData.DecreaseEnergy(_currentRecipe.ProductEnergy);
        }
    }

    private bool HasEnoughEnergy()
    {
        if (_currentRecipe == null || ResultItemOutput()) return false;
        else if (GameManager.Instance.GameData.Energy >= _currentRecipe.ProductEnergy) return true;
        else return false;
    }

    private bool ResultItemOutput()
    {
        if (_resultButton.interactable == true) return true;
        else return false;
    }

    /*
    crafting time deleted 

    /// <summary>
    /// Coroutine, UI bar Update for crafting time.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CraftingTime()
    {
        float process = 0f;
        while (process < 1f) {
            process += (float)Time.deltaTime / _currentRecipe.craftingTime;
            _sliderImage.fillAmount = Mathf.Lerp(0f, 1f, process);
            yield return null;
        }
        ResultPrint();
        _craftCoroutine = null;
    }
    */


    private void ResultPrint()
    {
        _resultImage.color = Color.white;
        _resultImage.sprite = _currentRecipe.ResultItem.Icon;
        _resultButton.interactable = true;
        _restrictPanel.SetActive(true);
    }

    public void GetItem()
    {
        if (_currentRecipe != null)
        {
            InventoryManager.Instance.AddItemToInventory(_currentRecipe.ResultItem);
            _restrictPanel.SetActive(false);
        }
    }

    public void GoToRepairMenu()
    {
        GameManager.Instance.InGameUIManager.ShowUI(UIType.Repair);
    }

    #region unused code

    // Due to the crafting system update, crafting only consume energy
    // code below is not used.
    // saved the code in case of reusing.

    /*

    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Button _craftButton;
    [SerializeField] private Image _sliderImage;
    [SerializeField] private Button _resultButton;
    [SerializeField] private Image _resultImage;

    private CraftingRecipe _currentRecipe;

    private Coroutine _craftCoroutine;

    private void Update()
    {
        UIUpdate();
        CraftingUIUpdate();
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
        if (_currentRecipe != null)
        {
            _image.sprite = _currentRecipe.resultItem.Icon;
            _nameText.text = _currentRecipe.resultItem.Name;
            _descriptionText.text = _currentRecipe.resultItem.Description;
        }
    }

    /// <summary>
    /// Update Create Button.
    /// If item is Craftable, interaction will be occur.
    /// </summary>
    private void CraftingUIUpdate()
    {
        bool isCraftable = AbleToMakeItem(RecipeRequireItemDictionary(_currentRecipe), InventoryItemDictionary());

        if (isCraftable) _craftButton.interactable = true;
        else _craftButton.interactable = false;
    }

    /// <summary>
    /// For OnClick button event. Craft Item.
    /// </summary>
    public void CraftItem()
    {
        if (_craftCoroutine == null)
        {
            ConsumeItem();
            _craftCoroutine = StartCoroutine(CraftingTime());            
        }
    }

    /// <summary>
    /// If recipe is craftable, consume material item.
    /// </summary>
    private void ConsumeItem()
    {
        bool isCraftable = AbleToMakeItem(RecipeRequireItemDictionary(_currentRecipe), InventoryItemDictionary());

        if (!isCraftable) return;

        int inventorySlot = InventoryManager.Instance.InventoryCount;

        Dictionary<ItemSO, int> reqItemsAndNum = RecipeRequireItemDictionary(_currentRecipe);

        foreach (var key in reqItemsAndNum.Keys)
        {
            InventoryManager.Instance.RemoveItemFromInventory(key, reqItemsAndNum[key]);
        }
        Debug.Log("제거됨");
    }

    /// <summary>
    /// Coroutine, UI bar Update for crafting time.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CraftingTime()
    {
        float process = 0f;
        while (process < 1f)
        {
            process += (float)Time.deltaTime / _currentRecipe.craftingTime;
            _sliderImage.fillAmount = Mathf.Lerp(0f, 1f, process);
            yield return null;
        }
        ResultPrint();
        _craftCoroutine = null;
    }

   
    private void ResultPrint()
    {
        _resultImage.color = Color.white;
        _resultImage.sprite = _currentRecipe.resultItem.Icon;
        _resultButton.interactable = true;
    }
   
    public void GetItem()
    {
        if (_currentRecipe != null)
        {
            InventoryManager.Instance.AddItemToInventory(_currentRecipe.resultItem);             
        }
    }

    
    /// <summary>
    /// Compare Dictionary of inventory item and Recipe require item.
    /// If there's enough materials for recipe, return true.
    /// </summary>
    /// <param name="reqItemsAndNum"></param>
    /// <param name="possessItemsAndNum"></param>
    /// <returns></returns>
    private bool AbleToMakeItem(Dictionary<ItemSO, int> reqItemsAndNum, Dictionary<ItemSO, int> possessItemsAndNum)
    {
        if (reqItemsAndNum == null || possessItemsAndNum == null) return false;

        foreach (var key in reqItemsAndNum.Keys)
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
    private Dictionary<ItemSO, int> RecipeRequireItemDictionary(CraftingRecipe recipe)
    {
        if (recipe == null) return null;

        Dictionary<ItemSO, int> reqItemsAndNum = new Dictionary<ItemSO, int>();

        int reqItemListCount = recipe.reqItem.Count;

        for (int i = 0; i < reqItemListCount; i++)
        {
            if (recipe.reqItem[i] == null) continue;

            ItemSO itemID = recipe.reqItem[i];
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
    private Dictionary<ItemSO, int> InventoryItemDictionary()
    {
        Dictionary<ItemSO, int> possessItemsAndNum = new Dictionary<ItemSO, int>();

        int inventorySlot = InventoryManager.Instance.InventoryCount;

        for (int i = 0; i < inventorySlot; i++)
        {
            ItemSO item = InventoryManager.Instance.ReadFromInventory(i, out int stack);

            if (item == null) continue;

            if (possessItemsAndNum.ContainsKey(item)) possessItemsAndNum[item] += stack;
            else possessItemsAndNum[item] = stack;
        }
        return possessItemsAndNum;
    }
    */

    #endregion
}