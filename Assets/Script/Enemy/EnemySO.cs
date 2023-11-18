using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy_", menuName = "Scriptable Object/Enemy")]

public class EnemySO : ScriptableObject
{
    public string enemyName;
    public float startMoveSpeed;
    public float startPower;
    public float startDetectiveRadius;
    public GameObject enemyPrefab;
}
