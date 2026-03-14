using UnityEngine;
using System;

// 这是一个纯 C# 类，负责作物的逻辑计算
[Serializable]
public abstract class Crop
{
    public CropData data;             // 引用 ScriptableObject 配置
    public int currentHealth;
    public int currentGrowthProgress;
    public int currentStage;
    public bool isDead => currentHealth <= 0;

    public Crop(CropData data)
    {
        this.data = data;
        this.currentHealth = data.maxHealth;
        this.currentGrowthProgress = 0;
        this.currentStage = 0;
    }

    // 每一个子类必须实现自己的收获逻辑
    public abstract void OnHarvest();

    // 生长逻辑
    public virtual void Grow()
    {
        if (currentGrowthProgress < data.daysToGrow)
            currentGrowthProgress++;

        currentStage = Mathf.Min(data.growthStages - 1,
            (currentGrowthProgress * data.growthStages) / data.daysToGrow);
    }

    // --- 任务 3 新增：计算最终产量的逻辑 ---
    public int GetFinalYield()
    {
        // 这里引用了 GlobalStats（全局静态变量）来计算 Roguelike 的加成
        float final = data.baseYield * GlobalStats.CropYieldMultiplier;

        // 如果是多年生作物，可以检查是否有特定加成
        if (data.isPerennial && GlobalStats.CropTypeMultipliers.ContainsKey("Perennial"))
        {
            final *= GlobalStats.CropTypeMultipliers["Perennial"];
        }

        return Mathf.RoundToInt(final);
    }
}

// 一年生作物子类
public class AnnualCrop : Crop
{
    public AnnualCrop(CropData data) : base(data) { }
    public override void OnHarvest()
    {
        currentHealth = 0; // 收获后即枯萎
        Debug.Log($"{data.cropName} 已收割，获得产量: {GetFinalYield()}");
    }
}

// 多年生作物子类
public class PerennialCrop : Crop
{
    public PerennialCrop(CropData data) : base(data) { }
    public override void OnHarvest()
    {
        // 收获后倒退生长进度，不死亡
        currentGrowthProgress = data.daysToGrow / 2;
        Debug.Log($"{data.cropName} 采摘完成，根系保留。预计产量: {GetFinalYield()}");
    }
}