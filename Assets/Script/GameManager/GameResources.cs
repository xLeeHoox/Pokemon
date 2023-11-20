using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;
    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            }
            return instance;
        }
    }

    [SerializeField] public CharacterSO player;
    [SerializeField] public EnemySO enemy;
    [SerializeField] public ItemSO pokemonItem;
    [SerializeField] public List<LevelSO> levelList;
    [SerializeField] public List<GameObject> rewardPrefab;
    [SerializeField] public List<GameObject> scorePrefab;
    [SerializeField] public List<LevelInfor> levelInfors;
    [SerializeField] public GameObject bloodEffect;
    public LevelSO selectedLevelSO;
    public float currentGem;
    public float currentGold;
    public void LoadData()
    {
        if (PlayerPrefs.HasKey("currentLevelIndex"))
        {
            int currentLevelIndex = PlayerPrefs.GetInt("currentLevelIndex");
            string currentIdleAnimName = PlayerPrefs.GetString("currentIdleAnimName");
            currentGem = PlayerPrefs.GetFloat("currentGem");
            currentGold = PlayerPrefs.GetFloat("currentGold");
            currentLevelSO = levelList[currentLevelIndex - 1];
            currentCharacterSO.idleAnimName = currentIdleAnimName;
            foreach (var item in pokemonItem.itemInfor)
            {
                if (item.idleAnimName == currentIdleAnimName)
                {
                    currentCharacterSO.characterSprite = item.itemImage;
                    currentCharacterSO.characterName = item.pokemonName;
                }
            }
        }

    }


}
