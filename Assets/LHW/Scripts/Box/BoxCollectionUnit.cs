using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxCollectionUnit : MonoBehaviour
{
    [SerializeField] Image _image;

    [SerializeField] BoxController _controller;

    [SerializeField] int _index;

    private CollectionSO _collection;

    public void Awake()
    {
        _image.color = Color.clear;
    }

    public void Update()
    {
        if(_controller.Data.BoxCollection[_index] != null)
        {
            _collection = _controller.Data.BoxCollection[_index];
        }
        else
        {
            _collection = null;
        }
    }

    public void UpdateUI(int index)
    {
        if(_collection == null)
        {
           _image.color = Color.clear;
        }
        else
        {
            _image.color = Color.white;
            _image.sprite = _collection.CollectionIcon;
        }
    }

    public void Onclick()
    {
        _controller.Data.GetCollection(_index);
    }
}