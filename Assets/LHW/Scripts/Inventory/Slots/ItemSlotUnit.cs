using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemSlotUnit : MonoBehaviour
{
    [SerializeField] protected int _index;

    [SerializeField] protected Image _image;
    [SerializeField] protected TMP_Text _text;

    protected ItemSO _item;
    public ItemSO Item => _item;

    protected int _itemStack;
    public int ItemStack => _itemStack;

    public abstract void Awake();

    public abstract void UpdateUI(int index);

    public abstract void Use(int index);
}
