using UnityEngine;
using System.Collections.Generic;

public class DisasterManager : MonoBehaviour
{
    public static DisasterManager Instance { get; private set; }

    private void Awake() => Instance = this;

    /// <summary>
    /// 根据季节触发灾害
    /// </summary>
    public void TriggerRandomDisaster(Season season)
    {
        float roll = Random.value; // 0.0 到 1.0 之间的随机数

        switch (season)
        {
            case Season.Spring:
                if (roll < 0.3f) ExecuteDisaster(DisasterType.Sandstorm, 15f); // 30% 概率沙尘暴
                break;
            case Season.Summer:
                if (roll < 0.4f) ExecuteDisaster(DisasterType.Drought, 20f);   // 40% 概率旱灾
                break;
            case Season.Autumn:
                if (roll < 0.2f) ExecuteDisaster(DisasterType.Locusts, 25f);   // 20% 概率蝗灾
                break;
            case Season.Winter:
                if (roll < 0.5f) ExecuteDisaster(DisasterType.Ice, 30f);       // 50% 概率冰冻
                break;
        }
    }

    private void ExecuteDisaster(DisasterType type, float damage)
    {
        Debug.Log($"<color=red>警告！发生灾害：{type}</color>");

        // 1. 扣除全局血量
        FieldHealthManager.Instance.TakeDamage(damage);

        // 2. 这里还可以遍历 FarmManager 里的地块，对作物造成伤害
    }
}