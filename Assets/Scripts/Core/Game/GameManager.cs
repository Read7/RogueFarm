using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // 单例模式，方便其他脚本调用：GameManager.Instance.AdvanceSeason();
    public static GameManager Instance { get; private set; }

    [Header("时间设定")]
    public int currentYear = 1;
    public Season currentSeason = Season.Spring;
    public int targetYear = 5;

    [Header("游戏状态")]
    public bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// 推进季节：游戏的核心驱动力
    /// </summary>
    public void AdvanceSeason()
    {
        if (isGameOver) return;

        // 1. 季节循环逻辑
        if (currentSeason == Season.Winter)
        {
            currentSeason = Season.Spring;
            currentYear++;

            if (currentYear > targetYear)
            {
                WinGame();
                return;
            }
        }
        else
        {
            currentSeason++;
        }

        Debug.Log($"进入新季节：第 {currentYear} 年 - {currentSeason}");

        // 2. 触发该季节的随机灾害
        DisasterManager.Instance.TriggerRandomDisaster(currentSeason);

        // 3. 通知所有地块生长 (可以通过事件系统或直接调用 FarmManager)
        // FindObjectOfType<FarmManager>().GrowAllCrops();
    }

    private void WinGame()
    {
        Debug.Log("恭喜！你成功经营农场度过了末日！");
        // 弹出胜利 UI
    }

    public void EndGame()
    {
        isGameOver = true;
        Debug.Log("农场已荒废，游戏结束。");
        // 弹出失败 UI
    }

    public void ProcessGameOver(SessionResult result)
    {
        isGameOver = true;

        // 1. 计算经验
        int earnedXP = result.CalculateGlobalXP();

        // 2. 更新持久化数据
        TalentManager.currentPlayerData.globalXP += earnedXP;

        // 3. 简单的升级判定
        if (TalentManager.currentPlayerData.globalXP >= 1000)
        {
            TalentManager.currentPlayerData.globalLevel++;
            TalentManager.currentPlayerData.globalXP -= 1000;
        }

        // 4. 存盘
        TalentManager.SaveData();

        // 5. 显示 UI（这里可以展示 result 的详情）
        // UIManager.Instance.ShowGameOverScreen(result);
    }
}

