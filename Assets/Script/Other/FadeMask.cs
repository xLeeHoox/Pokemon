using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeMask : MonoBehaviour
{
    public void OnEnable()
    {
        this.GetComponent<Image>().DOFade(0.5f, 3);
    }
}
