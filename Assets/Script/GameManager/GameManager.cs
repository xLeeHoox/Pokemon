using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using System.Linq;
using DG.Tweening;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    float currentTime = 0;
    int spawnCount = 0;
    public Player player;
    public List<Enemy> enemyList = new List<Enemy>();
    public List<Score> rankingList = new List<Score>();
    private List<Score> sortedRankingList;
    [SerializeField] public Transform mainCameraTransform;
    [SerializeField] public SpriteRenderer backgroundSprite;
    [SerializeField] public UltimateJoystick ultimateJoystick;
    public GameState currentGameState;
    Score top1Score = null;

    public void Start()
    {
        InitializePlayer(GameResources.Instance.currentCharacterSO);
        SetBackGround();
        currentGameState = GameState.Playing;

        GameResources.Instance.InitializeInterstitialAds();
    }
    public void OnDisable()
    {
        PlayerPrefs.SetFloat("currentGold", GameResources.Instance.currentGold);
        PlayerPrefs.SetFloat("currentGem", GameResources.Instance.currentGem);
        PlayerPrefs.SetInt("currentLevelIndex", GameResources.Instance.currentLevelSO.level);
        PlayerPrefs.SetString("currentIdleAnimName", GameResources.Instance.currentCharacterSO.idleAnimName);

    }
    public void Update()
    {
        switch (currentGameState)
        {
            case GameState.StartGame:
                {
                    break;
                }
            case GameState.Playing:
                {
                    SpawnByTime(3);
                    if (UpdateUI.Instance.GetComponentInChildren<CooldownClock>().IsOutOfTime())
                    {
                        GameResources.Instance.currentCharacterSO.characterLevel.IncreaseExp(200);
                        currentGameState = GameState.WinGame;
                    }
                    break;
                }
            case GameState.WinGame:
                {
                    StartCoroutine(OnWinGame());
                    break;
                }
            case GameState.LoseGame:
                {
                    StartCoroutine(OnLoseGame());
                    break;

                }
            case GameState.Waiting:
                {
                    break;
                }
            default:
                break;
        }

    }
    public void InitializePlayer(CharacterSO characterSO)
    {
        GameObject newPlayer = Instantiate(characterSO.playerPrefab, Vector3.zero, Quaternion.identity);
        player = newPlayer.GetComponent<Player>();
        newPlayer.gameObject.name = player.playerSO.characterName;
        player.UpdateImage(player.playerSO.characterSprite);
        player.animator.CrossFadeInFixedTime(GameResources.Instance.currentCharacterSO.idleAnimName, 0.25f, -1, 0f);
        rankingList.Add(player.score);
        player.powerController.SetCurrentPower(player.playerSO.startPower);
        player.dashAbility.FillMana(player.playerSO.maxMana);
        player.moveByVelocity.permanentSpeed = player.playerSO.startMoveSpeed;
        player.moveByVelocity.currentSpeed = player.moveByVelocity.permanentSpeed;
        virtualCamera.Follow = newPlayer.transform;
        UpdateUI.Instance.UpdateCurrentGem(GameResources.Instance.currentGem);
        UpdateUI.Instance.UpdateCurrentGold(GameResources.Instance.currentGold);

    }
    public void SpawnEnemy(EnemySO enemySO, float additionPower)
    {
        GameObject newEnemy = PoolManager.Instance.ReuseGameObject(enemySO.enemyPrefab, HelpUtilities.GetRandomPositionOutBoundary(mainCameraTransform.position, 20, 10, 1, 1));
        newEnemy.SetActive(true);
        newEnemy.name = HelpUtilities.enemyNames[Random.Range(0, 20)];
        Enemy enemy = newEnemy.GetComponent<Enemy>();
        enemyList.Add(enemy); //Add to enemy List so we could set up something to all enemy in the level mapj 
        rankingList.Add(enemy.score);
        enemy.powerController.SetCurrentPower(enemy.enemySO.startPower + additionPower);
        enemy.moveByVelocity.permanentSpeed = enemy.enemySO.startMoveSpeed;
        enemy.moveByVelocity.currentSpeed = enemy.moveByVelocity.permanentSpeed;
    }
    public void SpawnBuff(BuffSO buffSO)
    {
        Vector2 mainCameraPosion = mainCameraTransform.position;
        float yMaxBoundary = mainCameraPosion.y + 10;
        //float yMinBoundary = mainCameraPosion.y - 10;
        float xMaxBoundary = mainCameraPosion.x + 16;
        float xMinBoundary = mainCameraPosion.x - 16;

        GameObject newBuff = PoolManager.Instance.ReuseGameObject(buffSO.buffPrefab, new Vector2(Random.Range(xMinBoundary, xMaxBoundary), yMaxBoundary)); // Random spawn at the max boundary of main camera
        newBuff.SetActive(true);
        BuffTriggerEnter buffTriggerEnter = newBuff.GetComponent<BuffTriggerEnter>();
        buffTriggerEnter.buffSO = buffSO;

    }

    public void GenerateLevel(LevelSO levelSO)
    {
        if (player.isPlayerDead) return; // neu player dead thi return luon

        foreach (var item in levelSO.enemyList)
        {
            for (int i = 0; i < item.number; i++)
            {
                float currentPlayerPowerValue = player.powerController.currentPower;
                float[] valueList = { currentPlayerPowerValue * 1.1f, currentPlayerPowerValue / 1.1f };
                SpawnEnemy(item.spawnableObject, valueList[Random.Range(0, valueList.Length)]);
            }
        }
        int randomIndex = Random.Range(0, levelSO.buffList.Count); //Spawn random 1 type of buff
        for (int i = 0; i < levelSO.buffList[randomIndex].number; i++)
        {
            SpawnBuff(levelSO.buffList[randomIndex].spawnableObject);
        }

    }
    public void SpawnByTime(float intervalTime)
    {
        if (player.isPlayerDead) return; // neu player dead thi return luon

        if (enemyList.Count >= GameResources.Instance.currentLevelSO.maxEnemyNumber) return; // neu so luong enemy hien tai > max enemy thi return luon

        if ((Time.time - currentTime) >= intervalTime)
        {
            spawnCount++;
            GenerateLevel(GameResources.Instance.currentLevelSO);
            currentTime = Time.time;
        }
    }
    public void SetBackGround()
    {
        backgroundSprite.sprite = GameResources.Instance.selectedLevelSO.backgroundSprite;
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void Get05HighestScorePlayer()
    {
        sortedRankingList = rankingList.OrderByDescending(m => m.currentScore).ToList();
        if (sortedRankingList.Count < 5) return;
        for (int i = 0; i < 5; i++)

        {
            if (sortedRankingList[i] != null)
            {
                UpdateUI.Instance.rankingList[i].text = sortedRankingList[i].gameObject.name
                                                    + "   " + sortedRankingList[i].currentScore.ToString();
            }
            if (sortedRankingList[i] == null)
            {
                UpdateUI.Instance.rankingList[i].text = "";
            }
        }
        //tao hieu ung khi thay doi vi tri 1st
        if (top1Score == sortedRankingList[0]) return;
        Debug.Log("Hieu ung thay doi vi tri 1st");
        UpdateUI.Instance.rankingList[0].transform.DOShakeScale(1, 0.5f, 2).OnComplete(
            () => UpdateUI.Instance.rankingList[0].transform.localScale = new Vector3(1, 1, 1));
        top1Score = sortedRankingList[0];
    }
    IEnumerator OnWinGame()
    {
        UpdateUI.Instance.winPanel.SetActive(true);
        DisableAllPlayer();
        yield return new WaitForSeconds(3);
        GameResources.Instance.ShowAd();
        UpdateUI.Instance.OnWinGame();
        currentGameState = GameState.Waiting;
    }
    IEnumerator OnLoseGame()
    {
        UpdateUI.Instance.losePanel.SetActive(true);
        DisableAllPlayer();
        yield return new WaitForSeconds(3);
        GameResources.Instance.ShowAd();
        UpdateUI.Instance.OnLoseGame();
        currentGameState = GameState.Waiting;
    }

    public bool IsWinGame()
    {
        if (sortedRankingList[0].GetComponent<Player>() != null)
        {
            return true;
        }
        else return false;

    }
    public void DisableAllPlayer()
    {
        player.gameObject.SetActive(false);
        PoolManager.Instance.gameObject.SetActive(false);
        Debug.Log("Disable all player");
    }

}


