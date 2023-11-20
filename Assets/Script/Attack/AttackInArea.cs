using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackInArea : MonoBehaviour
{
    Player player;
    Enemy enemy;
    PlayerKillEffect playerKillEffect;
    public void Start()
    {
        player = GetComponentInParent<Player>();
        enemy = GetComponentInParent<Enemy>();
        playerKillEffect = GetComponentInParent<PlayerKillEffect>();


    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null) // truong hop player an enemy
        {
            Enemy enemyColision = collision.GetComponent<Enemy>();
            if (enemyColision != null)
            {
                if (player.powerController.currentPower >= enemyColision.powerController.currentPower)
                {
                    enemyColision.gameObject.SetActive(false);
                    GameManager.Instance.enemyList.Remove(enemyColision);
                    player.powerController.IncreasePower(enemyColision.powerController.currentPower);
                    UpdateAllEnemyColorPower();
                    playerKillEffect.OnKillEnemy();
                    playerKillEffect.ActiveBlood(collision.transform.position);
                    Player.killNumber++;
                    player.animator.SetBool("Eated", true);
                    UpdateUI.Instance.UpdateKillNumber(Player.killNumber);
                    GameManager.Instance.Get05HighestScorePlayer();
                }
            }
        }
        if (enemy != null) // truong hop enemy an enemy
        {
            Enemy enemyColision = collision.GetComponent<Enemy>();
            Player playerColision = collision.GetComponent<Player>();
            if (enemyColision != null)
            {
                if (enemy.powerController.currentPower >= enemyColision.powerController.currentPower)
                {
                    enemyColision.gameObject.SetActive(false);
                    GameManager.Instance.enemyList.Remove(enemyColision);
                    enemy.powerController.IncreasePower(enemyColision.powerController.currentPower);
                    enemy.score.IncreaseScore(100);
                    enemy.animator.SetBool("IsEaten", true);
                    GameManager.Instance.Get05HighestScorePlayer();

                }
            }
            if (playerColision != null)
            {
                if (enemy.powerController.currentPower >= playerColision.powerController.currentPower)
                {
                    playerColision.gameObject.SetActive(false);
                    GameManager.Instance.currentGameState = GameState.LoseGame;
                }
            }

        }

    }
    public void UpdateAllEnemyColorPower()
    {
        foreach (var item in GameManager.Instance.enemyList)
        {
            item.UpdatePowerColor();
        }
    }
}
