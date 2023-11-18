using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public MoveByVelocity moveByVelocity;
    [HideInInspector] public EnemySO enemySO;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public PowerController powerController;
    [HideInInspector] public EnemyInput enemyInput;
    [HideInInspector] public Score score;
    [HideInInspector] public Animator animator;


    public void Awake()
    {
        enemySO = GameResources.Instance.enemy;
        moveByVelocity = GetComponent<MoveByVelocity>();
        rb = GetComponent<Rigidbody2D>();
        powerController = GetComponentInChildren<PowerController>();
        enemyInput = GetComponent<EnemyInput>();
        score = GetComponent<Score>();
        animator = GetComponentInChildren<Animator>();
    }
    public void Update()
    {
        if (IsMoveToPlayerBoundary())
        {
            MoveToPlayer();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            MoveToPlayer();
        }
    }
    public bool IsMoveToPlayerBoundary()
    {
        Vector2 mainCameraPosion = GameManager.Instance.mainCameraTransform.position;
        float yMaxBoundary = mainCameraPosion.y + 10 + 4;
        float yMinBoundary = mainCameraPosion.y - 10 - 4;
        float xMaxBoundary = mainCameraPosion.x + 20 + 4;
        float xMinBoundary = mainCameraPosion.x - 20 - 4;
        Vector2 enemyPosition = this.transform.position;
        if (enemyPosition.x > xMinBoundary && enemyPosition.x < xMaxBoundary && enemyPosition.y > yMinBoundary && enemyPosition.y < yMaxBoundary)
        {
            return false;
        }
        else return true;
    }
    public bool IsMoveToCameraBoundary()
    {
        Vector2 mainCameraPosion = GameManager.Instance.mainCameraTransform.position;
        float yMaxBoundary = mainCameraPosion.y + 10;
        float yMinBoundary = mainCameraPosion.y - 10;
        float xMaxBoundary = mainCameraPosion.x + 20;
        float xMinBoundary = mainCameraPosion.x - 20;
        Vector2 enemyPosition = this.transform.position;
        if (enemyPosition.x > xMinBoundary && enemyPosition.x < xMaxBoundary && enemyPosition.y > yMinBoundary && enemyPosition.y < yMaxBoundary)
        {
            return false;
        }
        else return true;
    }
    public void MoveToPlayer()
    {
        Vector2 direction = (GameManager.Instance.player.transform.position - this.transform.position).normalized;
        moveByVelocity.CallMoveByVelocity(direction);

    }
    public void UpdatePowerColor()
    {
        if (powerController.currentPower <= GameManager.Instance.player.powerController.currentPower)
        {
            powerController.SetPowerColorText(Color.green);
        }
        else
        {
            powerController.SetPowerColorText(Color.red);
        }
    }

}
