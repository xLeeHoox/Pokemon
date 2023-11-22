using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class OtherItemInfor
{
    public string itemName;
    public Sprite itemImage;
    public bool isUnlock;
}
[CreateAssetMenu(fileName = "Item_", menuName = "Scriptable Object/Other Item")]
public class OtherItemSO : ScriptableObject
{
    public ItemType itemType;
    public GameObject itemPrefab;
    public float itemPrice;
    public OtherItemInfor[] otherItemInfors;

}
