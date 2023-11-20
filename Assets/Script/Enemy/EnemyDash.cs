using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDash : MonoBehaviour
{
    [SerializeField] GameObject dashTrailEffect;
    Enemy enemy;
    public bool isDashing = false;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    public void CallDash()
    {
        enemy.enemyInput.isAutoMove = false;
        enemy.moveByVelocity.SetSpeed(enemy.moveByVelocity.permanentSpeed * 2);
        dashTrailEffect.SetActive(true);
    }
    public void StopDash()
    {
        enemy.enemyInput.isAutoMove = false;
        enemy.moveByVelocity.SetSpeed(enemy.moveByVelocity.permanentSpeed);
        dashTrailEffect.SetActive(false);
    }
}
