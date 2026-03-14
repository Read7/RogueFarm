using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmManager : MonoBehaviour
{
    public Tilemap groundTilemap;    // 拖入你的地面 Tilemap
    public GameObject cropPrefab;    // 拖入你之前做的 Crop_Entity 预制件
    public CropData selectedSeed;    // 当前选中的种子（演示用）

    // 存储所有土地的数据：坐标 -> 土地实例
    private Dictionary<Vector3Int, FarmTile> farmTiles = new Dictionary<Vector3Int, FarmTile>();

    void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            HandlePlanting();
        }
    }

    void HandlePlanting()
    {
        // 将鼠标屏幕坐标转为世界坐标，再转为 Tilemap 的格子坐标
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = groundTilemap.WorldToCell(mouseWorldPos);

        // 1. 检查这块地是否已经初始化过数据
        if (!farmTiles.ContainsKey(gridPos))
        {
            farmTiles[gridPos] = new FarmTile { coordinate = (Vector2Int)gridPos };
        }

        FarmTile targetTile = farmTiles[gridPos];

        // 2. 如果地是空的，且玩家选了种子
        if (targetTile.state == TileState.Empty && selectedSeed != null)
        {
            // 逻辑层：播种
            targetTile.Plant(selectedSeed);

            // 表现层：生成图片物体
            // 我们把物体生成在格子的中心点
            Vector3 spawnPos = groundTilemap.GetCellCenterWorld(gridPos);
            GameObject go = Instantiate(cropPrefab, spawnPos, Quaternion.identity);

            // 核心步骤：将运行时的 Crop 数据传给显示脚本
            go.GetComponent<CropDisplay>().Initialize(targetTile.currentCrop);

            Debug.Log($"在 {gridPos} 种下了 {selectedSeed.cropName}");
        }
    }
}