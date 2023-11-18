using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    Enemy enemy;
    MoveByVelocity moveByVelocity;
    public Vector2 direction = Vector2.right;
    public bool isAutoMove = false;
    public bool isSLowed = false;

    public void OnEnable() //must use in OnEnable because Start method only call one time 
    {
        enemy = GetComponent<Enemy>();
        moveByVelocity = enemy.moveByVelocity;

    }
    public void Update()
    {
        if (!isAutoMove)
        {
            StopAllCoroutines();
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
}
