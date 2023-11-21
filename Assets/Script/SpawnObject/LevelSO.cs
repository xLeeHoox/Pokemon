using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct SpawnableObjectNumber<T>
{
    public T spawnableObject;
    public float number;
}
//[System.Serializable]
//public struct SpawnableObjectByLeve<T>
//{
//    public int level;
//    public List<SpawnableObjectNumber<T>> spawnableObjectNumbers;
//}
[CreateAssetMenu(fileName = "Level_", menuName = "Scriptable Object/Level")]
public class LevelSO : ScriptableObject
{
    public int level;
    public Sprite backgroundSprite;
    public float levelDuration;
    public int maxEnemyNumber;
    public List<SpawnableObjectNumber<EnemySO>> enemyList;
    public List<SpawnableObjectNumber<BuffSO>> buffList;
}
