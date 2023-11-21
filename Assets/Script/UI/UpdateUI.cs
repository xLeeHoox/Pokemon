using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UpdateUI : SingletonMonoBehavior<UpdateUI>
{
    [Header("Main Game Panel")]
    [SerializeField] Slider manabarSlider;
    [SerializeField] Transform doubleKill;
    [SerializeField] Transform tripleKill;
    [SerializeField] Transform quadraKill;
    [SerializeField] Transform pentaKill;
    [SerializeField] TextMeshProUGUI killNumber;
    [SerializeField] public TextMeshProUGUI currentGold;
    [SerializeField] public TextMeshProUGUI currentGem;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public GameObject winPanel;
    [SerializeField] public GameObject losePanel;
    [SerializeField] public GameObject mainGamePanel;
    [SerializeField] public List<TextMeshProUGUI> rankingList;
    [Header("Waiting Panel")]
    [SerializeField] public GameObject WaitingGamePanel;
    [SerializeField] public GameObject winGameText;
    [SerializeField] public TextMeshProUGUI rankingText;
    [SerializeField] public TextMeshProUGUI killNumberText;
    [SerializeField] public GameObject continuosButton;
    [SerializeField] public GameObject loseGameText;
    [SerializeField] public GameObject playAgainButton;
    [SerializeField] Image playerImage;
    [Header("Dash button")]
    [SerializeField] Button dashButton;




    public void UpdateManaBar(float value)
    {
        manabarSlider.value = value;
    }
    public void UpdateCurrentGold(float value)
    {
        currentGold.text = value.ToString();
    }
    public void UpdateCurrentGem(float value)
    {
        currentGem.text = value.ToString();
    }
    public void DoubleKillEffect()
    {
        doubleKill.gameObject.SetActive(true);
    }
    public void TripleKillEffect()
    {
        tripleKill.gameObject.SetActive(true);
    }
    public void QuadraKillEffect()
    {
        quadraKill.gameObject.SetActive(true);
    }
    public void PentaKillEffect()
    {
        pentaKill.gameObject.SetActive(true);
    }
    public void UpdateKillNumber(float value)
    {
        killNumber.text = "Kill: " + value.ToString();
    }
    public void UpdateScore(float value)
    {
        scoreText.text = value.ToString();
    }
    public void OnWinGame()
    {
        mainGamePanel.SetActive(false);
        WaitingGamePanel.SetActive(true);
        WaitingGamePanel.GetComponent<RectTransform>().DOScale(0, 0).OnComplete(() => WaitingGamePanel.GetComponent<RectTransform>().DOScale(1, 1));
        winGameText.SetActive(true);
        rankingText.text = "Ranking: " + (GameManager.Instance.GetRankingOfPlayer() + 1).ToString();
        killNumberText.text = "Kill: " + Player.killNumber.ToString();
        continuosButton.SetActive(true);
        UpdateUI.Instance.UpdatePlayerImage();

    }
    public void OnLoseGame()
    {
        mainGamePanel.SetActive(false);
        WaitingGamePanel.SetActive(true);
        WaitingGamePanel.GetComponent<RectTransform>().DOScale(0, 0).OnComplete(() => WaitingGamePanel.GetComponent<RectTransform>().DOScale(1, 1));
        loseGameText.SetActive(true);
        playAgainButton.SetActive(true);
        UpdateUI.Instance.UpdatePlayerImage();


    }
    public void UpdatePlayerImage()
    {
        playerImage.sprite = GameResources.Instance.currentCharacterSO.characterSprite;
    }

}
