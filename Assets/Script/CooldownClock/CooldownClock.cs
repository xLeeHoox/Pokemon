using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CooldownClock : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeValueText;
    [SerializeField] TextMeshProUGUI last10Second;
    float elapsedTime;
    public void Start()
    {
        elapsedTime = GameResources.Instance.currentLevelSO.levelDuration; // start elapsed time by level duration
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.Playing || elapsedTime < 1) return; //neu player die thi return luon        

        elapsedTime -= Time.deltaTime; // dem nguoc
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeValueText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (elapsedTime <= 10)
        {
            ActiveLast10Second();
        }
    }
    public bool IsOutOfTime()
    {
        return elapsedTime < 1; // het thoi gian man choi
    }
    public void ActiveLast10Second()
    {
        if (!last10Second.gameObject.activeSelf)
        {
            last10Second.gameObject.SetActive(true);
            last10Second.transform.DOScale(1, 1).SetLoops(10, LoopType.Restart).OnComplete(() => last10Second.gameObject.SetActive(false));
        }
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        last10Second.text = string.Format("{00}", seconds);
    }
}
