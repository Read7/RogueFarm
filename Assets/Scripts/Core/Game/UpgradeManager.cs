using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public float currentXP = 0;
    public float xpToNextLevel = 100;

    private void Awake() => Instance = this;

    public void AddExperience(float amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel) LevelUp();
    }

    private void LevelUp()
    {
        currentXP -= xpToNextLevel;
        xpToNextLevel *= 1.2f; // 典型的 Rogue 经验递增

        // 弹出 UI，展示三个随机 UpgradeDef
        Debug.Log("等级提升！请选择你的进化方向。");
    }

    public void ApplyUpgrade(UpgradeDef upgrade)
    {
        switch (upgrade.type)
        {
            case UpgradeType.Production:
                // 这里的逻辑：直接修改全局静态变量
                GlobalStats.CropYieldMultiplier += upgrade.value;
                Debug.Log($"全局产量提升至: {GlobalStats.CropYieldMultiplier * 100}%");
                break;

            case UpgradeType.Resistance:
                // 防滑坡网逻辑：降低受到的灾害伤害
                GlobalStats.DisasterDamageMultiplier -= upgrade.value;
                break;

            case UpgradeType.Special:
                // 特殊奖励逻辑：如立刻收获所有成熟作物
                // FindObjectOfType<FarmManager>().HarvestAllReady();
                break;
        }
    }
}