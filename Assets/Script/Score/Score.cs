using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public float currentScore;
    public void Start()
    {
        currentScore = 0;
        //UpdateUI.Instance.UpdateScore(0);
    }

    public void IncreaseScore(float value)
    {
        currentScore += value;
        //UpdateUI.Instance.UpdateScore(currentScore);

    }

}
