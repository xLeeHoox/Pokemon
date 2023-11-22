using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ItemOther : MonoBehaviour
{
    [HideInInspector] public OtherItemInfor otherItemInfor;
    [HideInInspector] public float price;
    [HideInInspector] public ItemType itemType;
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemPrice;
    [SerializeField] Button buyButton;
    [SerializeField] Button equipButton;
    [SerializeField] Button previewButton;
    void Start()
    {
        UpdateItemInfor();
    }
    public void UpdateItemInfor()
    {
        itemImage.sprite = otherItemInfor.itemImage;
        itemPrice.text = price.ToString();
        buyButton.onClick.AddListener(UnlockItem);
        previewButton.onClick.AddListener(CallPreviewItem);
        equipButton.onClick.AddListener(EquipItem);
        equipButton.onClick.AddListener(CallPreviewItem);
        if (otherItemInfor.isUnlock)
        {
            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);
        }
    }
    public void UnlockItem()
    {
        Debug.Log("Click button");
        if (GameResources.Instance.currentGold > price)
        {
            otherItemInfor.isUnlock = true;
            GameResources.Instance.currentGold -= price;
            MainMenuUI.Instance.UpdatePlayerInfor();
            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);
        }
    }
    public void CallPreviewItem()
    {
        if (itemType == ItemType.Pet)
        {
            PreviewItem(PokemonShop.Instance.previewPet);
            return;
        }
        if (itemType == ItemType.Trail)
        {
            PreviewItem(PokemonShop.Instance.previewTrail);
            return;
        }

    }
    public void PreviewItem(Image imagePreview)
    {
        imagePreview.transform.localScale = Vector3.zero;
        imagePreview.transform.DOScale(1, 0.5f);
        imagePreview.sprite = previewButton.GetComponent<Image>().sprite;
    }
    public void EquipItem()
    {
        GameResources.Instance.currentCharacterSO.traiPrefab = otherItemInfor.prefab;
    }
}
