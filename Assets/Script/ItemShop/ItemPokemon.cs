using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ItemPokemon : MonoBehaviour
{
    [SerializeField] Image pokemonImage;
    [SerializeField] TextMeshProUGUI pokemonPrice;
    [SerializeField] Button buyButton;
    [SerializeField] Button equipButton;
    [SerializeField] Button previewButton;

    public ItemInfor itemInfor;
    public float price;
    void Start()
    {
        UpdateItemInfor();
    }
    public void UpdateItemInfor()
    {
        pokemonImage.sprite = itemInfor.itemImage;
        pokemonPrice.text = price.ToString();
        buyButton.onClick.AddListener(UnlockItem);
        previewButton.onClick.AddListener(PreviewItem);
        equipButton.onClick.AddListener(EquipImage);
        equipButton.onClick.AddListener(PreviewItem);
        if (itemInfor.isUnlock)
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
            itemInfor.isUnlock = true;
            GameResources.Instance.currentGold -= price;
            MainMenuUI.Instance.UpdatePlayerInfor();
            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);
        }
    }
    public void PreviewItem()
    {
        PokemonShop.Instance.previewPokemon.transform.localScale = Vector3.zero;
        PokemonShop.Instance.previewPokemon.transform.DOScale(1, 0.5f);
        PokemonShop.Instance.previewPokemon.sprite = previewButton.GetComponent<Image>().sprite;
    }
    public void EquipImage()
    {
        GameResources.Instance.currentCharacterSO.characterSprite = itemInfor.itemImage;
        GameResources.Instance.currentCharacterSO.idleAnimName = itemInfor.idleAnimName;
        //Animator animator = player.GetComponentInChildren<Animator>();
        //AnimationChange animationChange = player.GetComponent<AnimationChange>();
        //animationChange.ChangeSprite(animationChange.GetAnimationClip(animator, "PlayerIdle"), itemInfor.idleImages); //chay duoc nhung ko build duoc do su dung thu vien unityEditor
        //animationChange.ChangeSprite(animationChange.GetAnimationClip(animator, "PlayerEat"), itemInfor.eatenImage);
        MainMenuUI.Instance.UpdatePlayerInfor();
    }


}
