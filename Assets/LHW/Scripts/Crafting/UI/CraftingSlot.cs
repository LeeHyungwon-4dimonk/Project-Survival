using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] TMP_Text _text;
    private void Awake()
    {
        _image.color = Color.clear;
        _text.text = "";
        gameObject.GetComponent<Button>().interactable = false;
    }

    public void OnClick()
    {
        GetComponentInParent<CraftingController>().GetItem();
        _image.color = Color.clear;
        _image.sprite = null;
        _text.text = "";
        gameObject.GetComponent<Button>().interactable = false;
    }
}
