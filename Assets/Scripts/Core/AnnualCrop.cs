using System;
using UnityEngine;

[Serializable]
public abstract class Crop
{
    public CropData data;             // 引用配置原型
    public int currentHealth;
    public int currentGrowthProgress; // 当前生长进度
    public int currentStage;          // 当前视觉阶段
    public bool isDead => currentHealth <= 0;

    public Crop(CropData data)
    {
        this.data = data;
        this.currentHealth = data.maxHealth;
        this.currentGrowthProgress = 0;
        this.currentStage = 0;
    }

    // 抽象逻辑：收获后发生什么？
    public abstract void OnHarvest();

    // 通用逻辑：每日生长
    public virtual void Grow()
    {
        if (currentGrowthProgress < data.daysToGrow)
            currentGrowthProgress++;

        // 更新视觉阶段计算
        currentStage = Mathf.Min(data.growthStages - 1,
            (currentGrowthProgress * data.growthStages) / data.daysToGrow);
    }
}

/// <summary>
/// 一年生作物：收获即死亡/移除
/// </summary>
public class AnnualCrop : Crop
{
    public AnnualCrop(CropData data) : base(data) { }

    public override void OnHarvest()
    {
        currentHealth = 0; // 标记为死亡，由 FarmTile 清理
        Debug.Log($"{data.cropName} 已收割，植株移除。");
    }
}

/// <summary>
/// 多年生作物：收获后进入休眠或重置阶段
/// </summary>
public class PerennialCrop : Crop
{
    public PerennialCrop(CropData data) : base(data) { }

    public override void OnHarvest()
    {
        // 多年生作物收获后，生长进度倒退回某个阶段，而不是消失
        currentGrowthProgress = data.daysToGrow / 2;
        Debug.Log($"{data.cropName} 已采摘，保留根系等待再次生长。");
    }
}