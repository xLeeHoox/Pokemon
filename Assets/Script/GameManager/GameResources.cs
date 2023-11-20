using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;
    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            }
            return instance;
        }
    }

    [SerializeField] public EnemySO enemy;
    [SerializeField] public ItemSO pokemonItem;
    [SerializeField] public List<LevelSO> levelList;
    [SerializeField] public List<GameObject> rewardPrefab;
    [SerializeField] public List<GameObject> scorePrefab;
    [SerializeField] public List<LevelInfor> levelInfors;
    [SerializeField] public GameObject bloodEffect;
    [SerializeField] public CharacterSO currentCharacterSO;
    public LevelSO currentLevelSO;
    public float currentGem;
    public float currentGold;
    public void LoadData()
    {
        if (PlayerPrefs.HasKey("currentLevelIndex"))
        {
            int currentLevelIndex = PlayerPrefs.GetInt("currentLevelIndex");
            string currentIdleAnimName = PlayerPrefs.GetString("currentIdleAnimName");
            currentGem = PlayerPrefs.GetFloat("currentGem");
            currentGold = PlayerPrefs.GetFloat("currentGold");
            currentLevelSO = levelList[currentLevelIndex - 1];
            currentCharacterSO.idleAnimName = currentIdleAnimName;
            foreach (var item in pokemonItem.itemInfor)
            {
                if (item.idleAnimName == currentIdleAnimName)
                {
                    currentCharacterSO.characterSprite = item.itemImage;
                    currentCharacterSO.characterName = item.pokemonName;
                }
            }
        }

    }

#if UNITY_IOS
string adUnitId = "YOUR_IOS_AD_UNIT_ID";
#else // UNITY_ANDROID
    string adUnitId = "ecb9e8eec74cdfbb";
#endif
    int retryAttempt;

    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    private void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(adUnitId);
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

        // Reset retry attempt
        retryAttempt = 0;
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

        retryAttempt++;
        double retryDelay = Mathf.Pow(2, Mathf.Min(6, retryAttempt));

        Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        LoadInterstitial();
    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
        LoadInterstitial();
    }

    public void ShowAd()
    {
        if (MaxSdk.IsInterstitialReady(adUnitId))
        {
            MaxSdk.ShowInterstitial(adUnitId);
        }
    }


}
