using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责动态生成网格地块，并将表现对象与 FarmTile 数据绑定。
/// </summary>
public class GridManager : MonoBehaviour
{
    [Header("网格配置")]
    [Min(1)]
    [SerializeField] private int gridWidth = 5;
    [Min(1)]
    [SerializeField] private int gridHeight = 5;
    [SerializeField] private GameObject tilePrefab;

    [Header("布局配置")]
    [SerializeField] private float tileSpacing = 1f;

    private readonly Dictionary<Vector2Int, FarmTile> tileDataMap = new Dictionary<Vector2Int, FarmTile>();

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        if (tilePrefab == null)
        {
            Debug.LogError("GridManager: tilePrefab 未设置，无法生成网格。");
            return;
        }

        Vector3 center = GetScreenCenterWorldPosition();
        float totalWidth = (gridWidth - 1) * tileSpacing;
        float totalHeight = (gridHeight - 1) * tileSpacing;
        Vector3 bottomLeft = center - new Vector3(totalWidth * 0.5f, totalHeight * 0.5f, 0f);

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Vector2Int coord = new Vector2Int(x, y);
                FarmTile tileData = new FarmTile { coordinate = coord };
                tileDataMap[coord] = tileData;

                Vector3 spawnPos = bottomLeft + new Vector3(x * tileSpacing, y * tileSpacing, 0f);
                GameObject tileObject = Instantiate(tilePrefab, spawnPos, Quaternion.identity, transform);

                TileVisual tileVisual = tileObject.GetComponent<TileVisual>();
                if (tileVisual == null)
                {
                    tileVisual = tileObject.AddComponent<TileVisual>();
                }

                tileVisual.Bind(tileData, coord);
            }
        }
    }

    private static Vector3 GetScreenCenterWorldPosition()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            return Vector3.zero;
        }

        if (cam.orthographic)
        {
            return new Vector3(cam.transform.position.x, cam.transform.position.y, 0f);
        }

        float depth = Mathf.Abs(cam.transform.position.z);
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, depth);
        Vector3 worldCenter = cam.ScreenToWorldPoint(screenCenter);
        worldCenter.z = 0f;
        return worldCenter;
    }

    public bool TryGetTileData(Vector2Int coordinate, out FarmTile tileData)
    {
        return tileDataMap.TryGetValue(coordinate, out tileData);
    }
}
