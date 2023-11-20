using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuffTriggerEnter : MonoBehaviour
{
    [HideInInspector] public BuffSO buffSO;
    public void Start()
    {
        Vector3 startPostiion = this.transform.position;
        Vector3 targetPosition = new Vector3(startPostiion.x, startPostiion.y - Random.Range(10, 20), startPostiion.z);
        this.transform.DOMove(targetPosition, 3).SetEase(Ease.InOutSine);
    }
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
