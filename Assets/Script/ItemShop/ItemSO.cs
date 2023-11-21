using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class ItemInfor
{
    public string pokemonName;
    public Sprite itemImage;
    public Sprite[] idleImages;
    public Sprite[] eatenImage;
    public string idleAnimName;
    public bool isUnlock;
}
[CreateAssetMenu(fileName = "Item_", menuName = "Scriptable Object/Item")]
public class ItemSO : ScriptableObject
{
    [FormerlySerializedAs("itemInfor")]
    public ItemInfor[] itemInfors;
    public float price;
    public ItemType itemType;
    public GameObject itemPrefab;
}
