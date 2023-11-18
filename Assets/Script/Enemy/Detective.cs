using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detective : MonoBehaviour
{
    [SerializeField] CircleCollider2D detectArea;
    [SerializeField] Transform detectAreaTransform;
    float tempMoveSpeed;
    public void Start()
    {
        SetDetectiveAreaRadius(2);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        Enemy enemy = this.GetComponentInParent<Enemy>();
        if (enemy == null) return; // avoid bug

        EnemyInput enemyInput = enemy.enemyInput;
        MoveByVelocity moveByVelocity = enemy.moveByVelocity;
        PowerController powerController = enemy.powerController;

        PowerController colisionPowerController = collision.GetComponentInChildren<PowerController>();
        PlayerBuffController playerBuffController = collision.GetComponent<PlayerBuffController>();
        if (playerBuffController != null && playerBuffController.isInvisible) // nếu colision là player và đang invisible thì return luôn
        {
            return;
        }
        if (colisionPowerController != null)
        {
            if (colisionPowerController.currentPower < powerController.currentPower)
            {
                Vector2 direction = (collision.transform.position - this.transform.position).normalized;
                enemyInput.direction = direction;
                //tempMoveSpeed = moveByVelocity.currentSpeed; // chứa tạm thời current speed để trả về lại tốc độ ban đầu
                moveByVelocity.IncreaseSpeed(moveByVelocity.currentSpeed * 0.1f);
                enemyInput.isAutoMove = false;
            }
            else if (colisionPowerController.currentPower > powerController.currentPower)
            {
                Vector2 direction = (collision.transform.position - this.transform.position).normalized;
                enemyInput.direction = -direction;
                //tempMoveSpeed = moveByVelocity.currentSpeed; // chứa tạm thời current speed để trả về lại tốc độ ban đầu
                moveByVelocity.IncreaseSpeed(moveByVelocity.currentSpeed * 0.0f);
                enemyInput.isAutoMove = false;
            }

        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {

        Enemy enemy = this.GetComponentInParent<Enemy>();
        if (enemy == null) return; //avoid bug
        EnemyInput enemyInput = enemy.enemyInput;
        MoveByVelocity moveByVelocity = enemy.moveByVelocity;
        EnemySO enemySO = enemy.enemySO;
        PowerController powerController = collision.GetComponentInChildren<PowerController>();

        if (powerController != null)
        {
            moveByVelocity.currentSpeed=moveByVelocity.permanentSpeed;
            enemyInput.isAutoMove = false;
        }

    }
    public void SetDetectiveAreaRadius(float value)
    {
        detectArea.radius = value;
        detectAreaTransform.localScale = new Vector3(2 * value, 2 * value, 2 * value);
    }
}
