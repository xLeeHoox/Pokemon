using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private MoveByVelocity moveByVelocity;
    private DashAbility dashAbility;
    private CharacterSO playerSO;
    private Vector2 playerDirection;
    private Player player;
    public void Awake()
    {
        player = GetComponent<Player>();
    }
    void Start()
    {
        playerSO = player.playerSO;
        moveByVelocity = player.moveByVelocity;
        dashAbility = player.dashAbility;
    }

    void Update()
    {
        if (player.isPlayerDead)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            return;
        }

        // MoveInputByKeyboard();
        MoveInputByJoystick();
        DashInput();
    }
    public void MoveInputByKeyboard()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        playerDirection = new Vector2(x, y);
        if (playerDirection != Vector2.zero)
        {
            moveByVelocity.CallMoveByVelocity(playerDirection);
        }
    }
    public void MoveInputByJoystick()
    {
        float x = GameManager.Instance.ultimateJoystick.HorizontalAxis;
        float y = GameManager.Instance.ultimateJoystick.VerticalAxis; ;
        if (x == 0 && y == 0) return;
        playerDirection = new Vector2(x, y).normalized;
        moveByVelocity.CallMoveByVelocity(playerDirection);

    }
    public void DashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashAbility.currentMana > 0.2f)
        {
            dashAbility.CallDash();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || dashAbility.currentMana <= 0.2f)
        {
            dashAbility.StopDash();
        }
    }

}
