using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    Enemy enemy;
    MoveByVelocity moveByVelocity;
    EnemyDash enemyDash;
    public Vector2 direction = Vector2.right;
    public bool isAutoMove = false;
    public bool isSLowed = false;

    public void OnEnable() //must use in OnEnable because Start method only call one time 
    {
        enemy = GetComponent<Enemy>();
        enemyDash = GetComponent<EnemyDash>();
        moveByVelocity = enemy.moveByVelocity;
    }

    public void Update()
    {
        if (GameManager.Instance.player.isPlayerDead)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            return;
        }
        if (!isAutoMove)
        {
            StopAllCoroutines();
            StartCoroutine(CallDashByTime(2));
            StartCoroutine(AutoMove());
        }
    }
    public IEnumerator AutoMove()
    {
        isAutoMove = true;
        while (true)
        {
            moveByVelocity.CallMoveByVelocity(direction);
            yield return new WaitForSeconds(Random.Range(3, 6));
            direction *= -1;
        }
    }
    public void RandomMove(Vector2 direction)
    {
        moveByVelocity.CallMoveByVelocity(direction);
    }
    public IEnumerator CallDashByTime(float t)
    {
        while (true)
        {
            yield return new WaitForSeconds(t);
            enemyDash.CallDash();
            yield return new WaitForSeconds(t);
            enemyDash.StopDash();
        }
    }
}
