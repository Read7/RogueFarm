using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropDisplay : MonoBehaviour
{
    private Crop runtimeCrop; // 运行时逻辑实例
    private SpriteRenderer spriteRenderer;

    [Header("不同生长阶段的图片")]
    public Sprite[] stageSprites;

    public void Initialize(Crop crop)
    {
        runtimeCrop = crop;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateAppearance();
    }

    // 模拟每回合调用
    public void OnTurnPassed()
    {
        if (runtimeCrop == null || runtimeCrop.isDead) return;

        runtimeCrop.Grow();
        UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        // 根据 runtimeCrop.currentStage 切换图片
        if (stageSprites.Length > runtimeCrop.currentStage)
        {
            spriteRenderer.sprite = stageSprites[runtimeCrop.currentStage];
        }
    }
}