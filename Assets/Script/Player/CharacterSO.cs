using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Character_", menuName = "Scriptable Object/Character")]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public Level characterLevel;
    public float startMoveSpeed;
    public float startPower;
    public GameObject playerPrefab;
    public float maxMana;
    public Sprite characterSprite;
    public string idleAnimName;
    public GameObject petPrefab;
    public GameObject traiPrefab;

}
