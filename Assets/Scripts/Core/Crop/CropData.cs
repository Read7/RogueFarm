using UnityEngine;
using System;
using System.Collections.Generic;

// --- 基础枚举定义 ---

public enum Season { Spring, Summer, Autumn, Winter }

public enum DisasterType
{
    None, Sandstorm, Flood, Locusts, Ice, Drought, Fire, Earthquake, AnimalAttack
}

public enum TileState { Empty, Planted, Fallow } // 荒废（Fallow）可用于土壤营养机制

// --- ScriptableObject 种子配置 ---

/// <summary>
/// 作物原始配置数据（只读原型）
/// </summary>
[CreateAssetMenu(fileName = "NewCropData", menuName = "RogueFarm/Crop Data")]
public class CropData : ScriptableObject
{
    [Header("基础信息")]
    public string cropName;
    public Sprite icon;

    [Header("生长设定")]
    public int maxHealth = 100;
    public int growthStages = 3;      // 生长阶段总数
    public int daysToGrow = 5;        // 总生长所需天数/步骤
    public int baseYield = 5;         // 基础产量

    [Header("季节限制")]
    public List<Season> plantableSeasons; // 哪些季节可以播种
    public List<Season> harvestSeasons;   // 哪些季节可以收获

    [Header("抗性数据 (0-1, 1为完全免疫)")]
    public float locustResistance = 0f;   // 抗蝗灾
    public float droughtResistance = 0f;  // 抗旱
    public float coldResistance = 0f;     // 抗寒

    public bool isPerennial;              // 是否为多年生
}