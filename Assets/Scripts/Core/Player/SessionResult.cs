using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SessionResult
{
    public int yearsSurvived;
    public int totalCropsHarvested;
    public int specialArtifactsCount;

    // 计算本次获得的全局经验值
    public int CalculateGlobalXP()
    {
        // 算法公式示例：(年数 * 100) + (作物 * 10) + (奖励品 * 50)
        return (yearsSurvived * 100) + (totalCropsHarvested * 10) + (specialArtifactsCount * 50);
    }
}