using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct LevelInfor
{
    public int level;
    public float expRequirement;
}
[System.Serializable]
public class Level
{
    public int currentLevel = 1;
    public float currentExp = 0;
    public void IncreaseExp(float exp)
    {
        currentExp += exp;
        List<LevelInfor> levelInfors = GameResources.Instance.levelInfors;
        CheckLevelUp(levelInfors);
    }
    public void CheckLevelUp(List<LevelInfor> leveInfors)
    {
        foreach (var item in leveInfors)
        {
            if (currentLevel == item.level)
            {
                if (currentExp >= item.expRequirement)
                {
                    currentLevel++;
                    currentExp = 0;
                }
            }
        }
    }
    public float GetCurrentExpRequirement()
    {
        List<LevelInfor> levelInfors = GameResources.Instance.levelInfors;
        foreach (var item in levelInfors)
        {
            if (currentLevel == item.level)
            {
                return item.expRequirement;
            }
        }
        return 0;
    }
}
