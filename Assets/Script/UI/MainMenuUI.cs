using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : SingletonMonoBehavior<MainMenuUI>
{
    [SerializeField] TextMeshProUGUI currentGoldInMainMenu;
    [SerializeField] TextMeshProUGUI currentGoldInShop;
    [SerializeField] TextMeshProUGUI currentLevel;
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI currentExp;
    [SerializeField] Image playerImage;
    void Start()
    {
        UpdatePlayerInfor();
    }

    void Update()
    {

    }
    public void UpdatePlayerInfor()
    {
        currentGoldInMainMenu.text = GameResources.Instance.currentGold.ToString();
        currentGoldInShop.text = GameResources.Instance.currentGold.ToString();
        currentLevel.text = GameResources.Instance.player.characterLevel.currentLevel.ToString();
        playerName.text = GameResources.Instance.player.characterName.ToString();
        currentExp.text = GameResources.Instance.player.characterLevel.currentExp.ToString() + "/500";
        playerImage.sprite = GameResources.Instance.player.characterSprite;
    }
}
