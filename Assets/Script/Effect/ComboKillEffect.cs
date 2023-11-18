using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ComboKillEffect : MonoBehaviour
{
    private void OnEnable()
    {
        Vector3 currentScale = this.transform.localScale;
        this.transform.localScale = Vector3.zero;
        this.transform.DOScale(currentScale, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }
}
