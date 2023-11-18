using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillEffect : MonoBehaviour
{
    public int comboKillCount = 0;
    public int comboCoolDownTime = 3;
    float currentTime = 0;
    private Score score;
    public void Awake()
    {
        score = GetComponent<Score>();
    }

    public void OnKillEnemy()
    {
        if (Time.time - currentTime > comboCoolDownTime)
        {
            comboKillCount = 1;
            GameObject point50 = PoolManager.Instance.ReuseGameObject(GameResources.Instance.scorePrefab[0], this.transform.position);
            point50.SetActive(true);
            GameObject normalKillReward = PoolManager.Instance.ReuseGameObject(GameResources.Instance.rewardPrefab[0], this.transform.position);
            normalKillReward.SetActive(true);
            score.IncreaseScore(50);
        }
        else
        {

            comboKillCount++;
        }
        currentTime = Time.time;

        switch (comboKillCount)
        {
            case 2:
                {
                    Debug.Log("Double kill");
                    UpdateUI.Instance.DoubleKillEffect();
                    GameObject point50 = PoolManager.Instance.ReuseGameObject(GameResources.Instance.scorePrefab[0], this.transform.position);
                    point50.SetActive(true);
                    GameObject doubleKillReward = PoolManager.Instance.ReuseGameObject(GameResources.Instance.rewardPrefab[1], this.transform.position);
                    doubleKillReward.SetActive(true);
                    score.IncreaseScore(50);

                    break;
                }
            case 3:
                {
                    Debug.Log("Triple kill");
                    UpdateUI.Instance.TripleKillEffect();
                    GameObject point75 = PoolManager.Instance.ReuseGameObject(GameResources.Instance.scorePrefab[1], this.transform.position);
                    point75.SetActive(true);
                    GameObject tripleKillReward = PoolManager.Instance.ReuseGameObject(GameResources.Instance.rewardPrefab[2], this.transform.position);
                    tripleKillReward.SetActive(true);
                    score.IncreaseScore(75);
                    break;
                }
            case 4:
                {
                    Debug.Log("Quadra kill");
                    UpdateUI.Instance.QuadraKillEffect();
                    GameObject point100 = PoolManager.Instance.ReuseGameObject(GameResources.Instance.scorePrefab[2], this.transform.position);
                    point100.SetActive(true);
                    GameObject quadraKillReward = PoolManager.Instance.ReuseGameObject(GameResources.Instance.rewardPrefab[3], this.transform.position);
                    quadraKillReward.SetActive(true);
                    score.IncreaseScore(100);
                    break;
                }
            case >= 5:
                {
                    Debug.Log("Penta kill");
                    UpdateUI.Instance.PentaKillEffect();
                    GameObject point150 = PoolManager.Instance.ReuseGameObject(GameResources.Instance.scorePrefab[3], this.transform.position);
                    point150.SetActive(true);
                    GameObject pentaKillReward = PoolManager.Instance.ReuseGameObject(GameResources.Instance.rewardPrefab[4], this.transform.position);
                    pentaKillReward.SetActive(true);
                    score.IncreaseScore(150);
                    break;
                }
            default:
                break;
        }
    }
    public void ActiveBlood(Vector2 position)
    {
        PoolManager.Instance.ReuseGameObject(GameResources.Instance.bloodEffect, position).SetActive(true);
    }

}
