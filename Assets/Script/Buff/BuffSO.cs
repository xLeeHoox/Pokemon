using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Buff_", menuName = "Scriptable Object/Buff")]
public class BuffSO : ScriptableObject
{
    public string buffName;
    public GameObject buffPrefab;
    public BuffType buffType;

    public void ApplyBuff(IBuffController buffController)
    {
        switch (buffType)
        {
            case BuffType.IncreasePower:
                {
                    buffController.IncreasePower();
                }
                break;
            case BuffType.IncreaseSpeed:
                {
                    buffController.IncreaseMoveSpeed();
                }
                break;
            case BuffType.IncreaseMana:
                {
                    buffController.IncreaseMana(2);
                }
                break;
            case BuffType.Invisible:
                {
                    buffController.ActiveInvisible();
                }
                break;
            case BuffType.Frezee:
                {
                    buffController.ActiveFrezee();
                }
                break;
            default:
                break;
        }
    }
}
