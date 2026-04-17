using UnityEngine;

/// <summary>
/// 挂载在地块表现对象上，持有对应逻辑数据引用。
/// </summary>
public class TileVisual : MonoBehaviour
{
    [Header("运行时数据 (调试用)")]
    [SerializeReference]
    public FarmTile tileData;

    [SerializeField]
    private Vector2Int gridCoordinate;

    [Header("默认种子数据")]
    [SerializeField]
    private CropData defaultSeedData;

    public Vector2Int GridCoordinate => gridCoordinate;

    public void Bind(FarmTile data, Vector2Int coordinate)
    {
        tileData = data;
        gridCoordinate = coordinate;
    }

    private void OnMouseDown()
    {
        if (tileData == null) return;
        if (tileData.state != TileState.Empty) return;

        CropData seed = defaultSeedData;
        if (seed == null)
        {
            Debug.LogWarning("TileVisual: 未设置 defaultSeedData，跳过播种");
            return;
        }

        tileData.Plant(seed);
        Debug.Log($"在 {gridCoordinate.x},{gridCoordinate.y} 位置种下了作物");
    }
}
