using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonShop : SingletonMonoBehavior<PokemonShop>
{
    [Header("PokemonShop")]
    [SerializeField] Transform pokemonNormalParent;
    [SerializeField] public Image previewPokemon;
    [Header("PetShop")]
    [SerializeField] Transform petNormalParent;
    [SerializeField] public Image previewPet;
    [Header("TrailShop")]
    [SerializeField] Transform trailNormalParent;
    [SerializeField] public Image previewTrail;
    public void Start()
    {
        GeneratePokemonShop();
        GenerateOtheItemShop(GameResources.Instance.trailItem, trailNormalParent);
        GenerateOtheItemShop(GameResources.Instance.petItem, petNormalParent);

    }
    public void GeneratePokemonShop()
    {
        ItemSO itemSO = GameResources.Instance.pokemonItem;
        for (int i = 0; i < itemSO.itemInfors.Length; i++)
        {
            GameObject newItem = Instantiate(itemSO.itemPrefab);
            newItem.transform.SetParent(pokemonNormalParent, false);
            ItemPokemon itemPokemon = newItem.GetComponent<ItemPokemon>();
            itemPokemon.itemInfor = itemSO.itemInfors[i];
            itemPokemon.price = itemSO.price;
        }
    }
    public void GenerateOtheItemShop(OtherItemSO otherItemSO, Transform parentTransform)
    {
        for (int i = 0; i < otherItemSO.otherItemInfors.Length; i++)
        {
            GameObject newItem = Instantiate(otherItemSO.itemPrefab);
            newItem.transform.SetParent(parentTransform, false);
            ItemOther itemPokemon = newItem.GetComponent<ItemOther>();
            itemPokemon.otherItemInfor = otherItemSO.otherItemInfors[i];
            itemPokemon.price = otherItemSO.itemPrice;
            itemPokemon.itemType = otherItemSO.itemType;

        }
    }
}
