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
    public void Start()
    {
        GeneratePokemonShop();
    }
    public void GeneratePokemonShop()
    {
        ItemSO itemSO = GameResources.Instance.pokemonItem;
        for (int i = 0; i < 4; i++)
        {
            GameObject newItem = Instantiate(itemSO.itemPrefab);
            newItem.transform.SetParent(pokemonNormalParent,false);
            ItemPokemon itemPokemon = newItem.GetComponent<ItemPokemon>();
            itemPokemon.itemInfor = itemSO.itemInfor[i];
            itemPokemon.price = itemSO.price;
        }
    }
    public void GeneratePetShop()
    {
        ItemSO itemSO = GameResources.Instance.pokemonItem;
        for (int i = 0; i < 8; i++)
        {
            GameObject newItem = Instantiate(itemSO.itemPrefab);
            newItem.transform.SetParent(pokemonNormalParent);
            ItemPokemon itemPokemon = newItem.GetComponent<ItemPokemon>();
            itemPokemon.itemInfor = itemSO.itemInfor[i];
            itemPokemon.price = itemSO.price;
        }
    }
}
