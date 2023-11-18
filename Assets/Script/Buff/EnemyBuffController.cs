using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuffController : MonoBehaviour, IBuffController
{
    private Enemy enemy;
    public void OnEnable()
    {
        enemy = GetComponent<Enemy>(); // không thể gọi ở start vì hàm Spawn Enemy gọi ở Start

    }
    public void Start()
    {

    }
    public void IncreaseMana(float value)
    {
    }

    public void IncreaseMoveSpeed()
    {
        StartCoroutine(IncreaseMoveSpeedByTime(1f));
    }

    public void IncreasePower()
    {
        enemy.powerController.IncreasePower(enemy.powerController.currentPower * 0.1f);

    }
    public IEnumerator IncreaseMoveSpeedByTime(float time)
    {
        enemy.moveByVelocity.IncreaseSpeed(enemy.moveByVelocity.currentSpeed * 0.5f);
        enemy.enemyInput.isAutoMove = false;
        yield return new WaitForSeconds(time);
        enemy.moveByVelocity.IncreaseSpeed(-enemy.moveByVelocity.currentSpeed * 0.5f);
        enemy.enemyInput.isAutoMove = false;

    }

    public void ActiveInvisible()
    {

    }

    public void ActiveFrezee()
    {

    }
}
