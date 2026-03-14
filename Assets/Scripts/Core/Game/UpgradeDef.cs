using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType { Production, Resistance, Special }

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "RogueFarm/Upgrade")]
public class UpgradeDef : ScriptableObject
{
    public string upgradeName;
    public string description;
    public UpgradeType type;
    public float value; // 增加的数值，如 0.2f 表示 20%
}