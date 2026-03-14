using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int globalLevel = 1;
    public int globalXP = 0;

    // 记录天赋等级：天赋 ID -> 当前等级
    public int talentOldFarmerLevel = 0; // 老农经验
    public int talentHardySoilLevel = 0; // 坚固土壤
}

public class TalentManager : MonoBehaviour
{
    public static PlayerData currentPlayerData = new PlayerData();

    // 在游戏启动时加载
    public static void LoadData()
    {
        string json = PlayerPrefs.GetString("RogueFarm_Save", "");
        if (!string.IsNullOrEmpty(json))
        {
            currentPlayerData = JsonUtility.FromJson<PlayerData>(json);
        }
    }

    // 保存数据
    public static void SaveData()
    {
        string json = JsonUtility.ToJson(currentPlayerData);
        PlayerPrefs.SetString("RogueFarm_Save", json);
        PlayerPrefs.Save();
    }

    // 应用天赋到全局静态变量（在每局游戏开始时调用）
    public static void ApplyPermanentTalents()
    {
        // 应用“老农经验”：金币加成
        // GlobalStats.StartingGold += currentPlayerData.talentOldFarmerLevel * 100;

        // 应用“坚固土壤”：田地血条上限
        // FieldHealthManager.Instance.maxHealth += currentPlayerData.talentHardySoilLevel * 50;
    }
}