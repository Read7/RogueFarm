using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalStats
{
    // 静态变量，所有 Crop 实例在计算产量时都会引用它
    public static float CropYieldMultiplier = 1.0f;
    public static float DisasterDamageMultiplier = 1.0f;

    // 专门针对某类作物的字典，例如 "Annual" -> 1.2f
    public static Dictionary<string, float> CropTypeMultipliers = new Dictionary<string, float>();
}