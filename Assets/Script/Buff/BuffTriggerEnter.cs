using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTriggerEnter : MonoBehaviour
{
    [HideInInspector] public BuffSO buffSO;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IBuffController buffController = collision.GetComponentInParent<IBuffController>();
        if (buffController != null)
        {
            buffSO.ApplyBuff(buffController);
            this.gameObject.SetActive(false);
        }
    }
}
