using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonMonoBehavior<SceneController>
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void StageSelection(LevelSO levelSO)
    {
        GameResources.Instance.currentLevelSO = levelSO;
    }
}
