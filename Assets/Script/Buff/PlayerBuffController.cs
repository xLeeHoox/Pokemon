using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffController : MonoBehaviour, IBuffController
{
    Player player;
    [HideInInspector] public bool isInvisible = false;

    public void Start()
    {
        player = GetComponent<Player>();

    }
    public void IncreasePower()
    {
        player.powerController.IncreasePower(player.powerController.currentPower * 0.1f);
    }
    public void IncreaseMana(float value)
    {
        player.dashAbility.FillMana(value);
    }
    public void IncreaseMoveSpeed()
    {
        StartCoroutine(IncreaseMoveSpeedByTime(4));
    }
    public IEnumerator IncreaseMoveSpeedByTime(float time)
    {
        player.moveByVelocity.IncreaseSpeed(player.moveByVelocity.currentSpeed * 0.5f);
        yield return new WaitForSeconds(time);
        player.moveByVelocity.currentSpeed = player.moveByVelocity.permanentSpeed;
    }
    public void ActiveInvisible()
    {
        StartCoroutine(ActiveInvisibleByTime(4));
    }
    public IEnumerator ActiveInvisibleByTime(float time)
    {
        isInvisible = true;
        SpriteRenderer playerImage = GetComponentInChildren<SpriteRenderer>();
        Color32 currentColor = playerImage.color;
        playerImage.color = new Color32((byte)currentColor.r, (byte)currentColor.g, (byte)currentColor.b, 100); //transparent from 255 to 100
        yield return new WaitForSeconds(time);
        isInvisible = false;
        playerImage.color = currentColor;

    }

    public void ActiveFrezee()
    {
        StartCoroutine(ActiveFrezeeByTime(4));
    }
    public IEnumerator ActiveFrezeeByTime(float time)
    {
        List<Enemy> enemyList = GameManager.Instance.enemyList;
        foreach (var enemy in enemyList)
        {
            enemy.moveByVelocity.currentSpeed *= 0.25f;
            enemy.enemyInput.isAutoMove = false;
        }
        yield return new WaitForSeconds(time);

        foreach (var enemy in enemyList)
        {
            enemy.moveByVelocity.currentSpeed = enemy.moveByVelocity.permanentSpeed;
            enemy.enemyInput.isAutoMove = false;

        }
    }
}
