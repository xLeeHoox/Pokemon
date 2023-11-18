using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableObject : MonoBehaviour
{
    [SerializeField] RewardType rewardType;
    [SerializeField] float rewardValue;
    private RectTransform goldRectTransform;
    public void Start()
    {
        goldRectTransform = UpdateUI.Instance.currentGem.GetComponent<RectTransform>();
    }
    public void OnEnable()
    {
        float randomValue = Random.Range(5, 11) * 0.1f * 0.3f;
        Vector3 randomScale = new Vector3(randomValue, randomValue, randomValue);
        this.transform.localScale = randomScale;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (rewardType)
        {
            case RewardType.SmallGem:
                {
                    float currentGem = GameResources.Instance.currentGem += rewardValue;
                    UpdateUI.Instance.UpdateCurrentGem(currentGem);
                    break;
                }
            case RewardType.BigGem:
                {
                    float currentGem = GameResources.Instance.currentGem += rewardValue;
                    UpdateUI.Instance.UpdateCurrentGem(currentGem);
                    break;
                }
            case RewardType.SmallGold:
                {
                    float currentGold = GameResources.Instance.currentGold += rewardValue;
                    UpdateUI.Instance.UpdateCurrentGold(currentGold);
                    break;
                }
            case RewardType.BigGold:
                {
                    float currentGold = GameResources.Instance.currentGold += rewardValue;
                    UpdateUI.Instance.UpdateCurrentGold(currentGold);
                    break;
                }
            default:
                break;
        }
        CollectEffect();
    }
    public void CollectEffect()
    {
        if (goldRectTransform != null)
        {
            Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(goldRectTransform.position);
            this.transform?.DOScale(0, 1);
            this.transform?.DOMove(cameraPosition, 1).OnComplete(() => gameObject.SetActive(false));
        }
    }


}
