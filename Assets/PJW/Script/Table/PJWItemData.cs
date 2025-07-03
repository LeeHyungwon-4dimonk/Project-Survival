using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemData
{
    public int    ItemID;
    public string ItemName;
    public string ItemType;
    public string ItemSprite;
    public string ItemTooltip;
    public int    ItemEnergy;
    public float  ItemWeight;
    public int    MaxPayloadPerPanel;
    public bool   IsDecomposable;
    public int    ItemStats;
    public string SpritePath;
    public string PrefabPath;
}
