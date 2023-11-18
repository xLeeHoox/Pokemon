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


}
