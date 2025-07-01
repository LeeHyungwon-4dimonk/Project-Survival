using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private BoxSlotUnit[] _slots;
    [SerializeField] private BoxSystem _data;

    private void Start()
    {
        _data.OnBoxSlotUpdated += UpdateUISlot;
        UpdateUISlot();
    }

    private void OnEnable()
    {
        _data.OnBoxSlotUpdated += UpdateUISlot;
        UpdateUISlot();
    }

    private void OnDisable()
    {
        _data.OnBoxSlotUpdated -= UpdateUISlot;
    }

    private void UpdateUISlot()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].UpdateUI(i);
        }
    }
}
