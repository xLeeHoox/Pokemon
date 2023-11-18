using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CooldownClock : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeValueText;
    float elapsedTime;
    public void Start()
    {
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeValueText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public bool IsOutOfTime()
    {
        return elapsedTime >= GameResources.Instance.selectedLevelSO.levelDuration;
    }
}
