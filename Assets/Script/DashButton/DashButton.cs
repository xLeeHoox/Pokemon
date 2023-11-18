using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DashButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Call Dash");
        CallDash();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Stop dash");
        StopDash();
    }
    public void CallDash()
    {
        DashAbility dashAbility = GameManager.Instance.player.dashAbility;
        if (dashAbility.currentMana >= 0.2f)
        {
            dashAbility.CallDash();
        }
    }
    public void StopDash()
    {
        DashAbility dashAbility = GameManager.Instance.player.dashAbility;
        dashAbility.StopDash();

    }
}
