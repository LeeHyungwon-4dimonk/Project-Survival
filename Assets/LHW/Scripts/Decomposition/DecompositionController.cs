using UnityEngine;

public class DecompositionController : MonoBehaviour
{
    [SerializeField] private DecompositionSlotUnit[] _slots;
    [SerializeField] private DecompositionSystem _data;

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

    private void UpdateUISlot()
    {
        for(int i = 0; i < _slots.Length; i++)
        {
            _slots[i].UpdateUI(i);
        }
    }
}