using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemInfor
{
    public Sprite itemImage;
    public bool isUnlock;
}
[CreateAssetMenu(fileName = "Item_", menuName = "Scriptable Object/Item")]
public class ItemSO : ScriptableObject
{
    public ItemInfor[] itemInfor;
    public float price;
    public ItemType itemType;
    public GameObject itemPrefab;
}
