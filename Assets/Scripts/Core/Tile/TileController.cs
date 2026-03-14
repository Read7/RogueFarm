using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂载在田地预制体上的控制器
/// 负责处理：玩家交互、持有地块数据、更新作物显示
/// </summary>
public class TileController : MonoBehaviour
{
    [Header("配置数据")]
    public Vector2Int gridCoordinate; // 这块地的坐标

    [Header("运行时数据 (仅供调试查看)")]
    // 使用 [SerializeReference] 可以让子类（一年生/多年生）在 Inspector 中正确显示
    [SerializeReference]
    public FarmTile tileData;

    private CropDisplay cropDisplay;

    private void Awake()
    {
        // 初始化这块地的逻辑数据
        tileData = new FarmTile();
        tileData.coordinate = gridCoordinate;

        // 获取挂载在同一个物体（或子物体）上的显示组件
        cropDisplay = GetComponentInChildren<CropDisplay>();
    }

    /// <summary>
    /// 外部调用：播种
    /// </summary>
    /// <param name="seedData">从 ScriptableObject 传入的种子配置</param>
    public void PlantSeed(CropData seedData)
    {
        if (tileData.state != TileState.Empty)
        {
            Debug.LogWarning("这块地已经种了东西！");
            return;
        }

        // 1. 调用逻辑层进行播种逻辑
        tileData.Plant(seedData);

        // 2. 通知显示层进行初始化
        if (cropDisplay != null && tileData.currentCrop != null)
        {
            cropDisplay.Initialize(tileData.currentCrop);
            Debug.Log($"成功播种: {seedData.cropName}");
        }
    }

    /// <summary>
    /// 模拟回合结束，作物生长
    /// </summary>
    public void OnTurnEnd()
    {
        if (tileData.currentCrop != null && !tileData.currentCrop.isDead)
        {
            // 如果你还没在 CropDisplay 里写 OnTurnPassed，先注释掉这行
            cropDisplay.OnTurnPassed();
        }
    }
}