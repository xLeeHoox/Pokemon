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
        InitializePlayer(GameResources.Instance.player);
        SetBackGround();
        currentGameState = GameState.Playing;
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
                        GameResources.Instance.player.characterLevel.IncreaseExp(200);
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
        player.UpdateImage(player.playerSO.characterSprite);
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
        float yMinBoundary = mainCameraPosion.y - 10;
        float xMaxBoundary = mainCameraPosion.x + 16;
        float xMinBoundary = mainCameraPosion.x - 16;

        GameObject newBuff = PoolManager.Instance.ReuseGameObject(buffSO.buffPrefab, new Vector2(Random.Range(xMinBoundary, xMaxBoundary), Random.Range(yMinBoundary, yMaxBoundary)));
        newBuff.SetActive(true);
        BuffTriggerEnter buffTriggerEnter = newBuff.GetComponent<BuffTriggerEnter>();
        buffTriggerEnter.buffSO = buffSO;

    }

    public void GenerateLevel(LevelSO levelSO)
    {
        foreach (var item in levelSO.enemyList)
        {
            for (int i = 0; i < item.number; i++)
            {
                SpawnEnemy(item.spawnableObject, Random.Range(1, 50) * spawnCount);
            }
        }
        foreach (var item in levelSO.buffList)
        {
            for (int i = 0; i < item.number; i++)
            {
                SpawnBuff(item.spawnableObject);
            }
        }
    }
    public void SpawnByTime(float intervalTime)
    {

        if ((Time.time - currentTime) >= intervalTime)
        {
            spawnCount++;
            Debug.Log("Generate level");
            GenerateLevel(GameResources.Instance.selectedLevelSO);
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
        UpdateUI.Instance.OnWinGame();
        currentGameState = GameState.Waiting;
    }
    IEnumerator OnLoseGame()
    {
        UpdateUI.Instance.losePanel.SetActive(true);
        DisableAllPlayer();
        yield return new WaitForSeconds(3);
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


