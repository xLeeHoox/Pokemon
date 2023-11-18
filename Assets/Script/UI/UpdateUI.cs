using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] public GameObject winGameButton;
    [SerializeField] public GameObject loseGameText;
    [SerializeField] public GameObject loseGameButton;
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
        winGameText.SetActive(true);
        winGameButton.SetActive(true);
        UpdateUI.Instance.UpdatePlayerImage();

    }
    public void OnLoseGame()
    {
        mainGamePanel.SetActive(false);
        WaitingGamePanel.SetActive(true);
        loseGameText.SetActive(true);
        loseGameButton.SetActive(true);
        UpdateUI.Instance.UpdatePlayerImage();


    }
    public void UpdatePlayerImage()
    {
        playerImage.sprite = GameResources.Instance.player.characterSprite;
    }

}
