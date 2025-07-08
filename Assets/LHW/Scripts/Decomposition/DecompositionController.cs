using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecompositionController : UIBase
{
    [SerializeField] private DecompositionSlotUnit[] _slots;
    [SerializeField] private DecompositionSystem _data;
    [SerializeField] private Image _energyBarImage;
    [SerializeField] private TMP_Text _energyBarText;

    private void Start()
    {
        _data.OnDecompositionSlotUpdated += UpdateUISlot;
        UpdateUISlot();
    }

    private void OnEnable()
    {
        _data.OnDecompositionSlotUpdated += UpdateUISlot;
        UpdateUISlot();
    }

    private void OnDisable()
    {
        _data.OnDecompositionSlotUpdated -= UpdateUISlot;
    }

    private void Update()
    {
        UpdateEnergyBarUI();
    }

    private void UpdateUISlot()
    {
        for (int i = 0; i < _slots.Length; i++) {
            _slots[i].UpdateUI(i);
        }
    }

    private void UpdateEnergyBarUI()
    {
        _energyBarImage.fillAmount = (float) GameManager.Instance.GameData.Energy / GameManager.Instance.GameData.MaxEnergy;
        _energyBarText.text = $"¿¡³ÊÁö : {GameManager.Instance.GameData.Energy}";
    }
}