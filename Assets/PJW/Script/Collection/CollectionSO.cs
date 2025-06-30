using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collection", menuName = "Collection/Create New Collection")]
public class CollectionSO : ScriptableObject
{
    public int CollectionId;
    public string CollectionName;
    public string CollectionDescription;
    public Sprite CollectionIcon;
    public Sprite SilhouetteSprite;
}
