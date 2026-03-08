using System;
using UnityEngine;

[Serializable]
public class FarmTile
{
    public Vector2Int coordinate;    // 田地坐标
    public TileState state = TileState.Empty;
    public Crop currentCrop;         // 当前作物引用

    /// <summary>
    /// 播种逻辑
    /// </summary>
    public void Plant(CropData seedData)
    {
        if (state != TileState.Empty) return;

        // 根据数据类型工厂化创建实例
        if (seedData.isPerennial)
            currentCrop = new PerennialCrop(seedData);
        else
            currentCrop = new AnnualCrop(seedData);

        state = TileState.Planted;
    }

    /// <summary>
    /// 处理灾害损失
    /// </summary>
    public void ProcessDisaster(DisasterType disaster, float intensity)
    {
        if (currentCrop == null || currentCrop.isDead) return;

        float damage = intensity * 10f; // 基础伤害数值

        // 简单的抗性判定示例
        if (disaster == DisasterType.Locusts) damage *= (1 - currentCrop.data.locustResistance);
        if (disaster == DisasterType.Drought) damage *= (1 - currentCrop.data.droughtResistance);

        currentCrop.currentHealth -= Mathf.RoundToInt(damage);

        if (currentCrop.isDead) state = TileState.Fallow;
    }
}