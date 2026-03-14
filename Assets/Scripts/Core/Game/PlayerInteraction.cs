using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType { Weed, Water, Fertilize, SetTrap }

public class PlayerInteraction : MonoBehaviour
{
    public float xpPerAction = 10f;

    public void Interact(FarmTile tile, InteractionType action)
    {
        if (tile.state == TileState.Empty) return;

        switch (action)
        {
            case InteractionType.Water:
                // 逻辑：增加当前作物的健康值，或提供“加速生长”Buff
                tile.currentCrop.currentHealth = Mathf.Min(tile.currentCrop.currentHealth + 10, tile.currentCrop.data.maxHealth);
                break;
            case InteractionType.Fertilize:
                // 逻辑：通过简单的标记位，让下一次收获产量翻倍
                // tile.isFertilized = true;
                break;
        }

        // 获得经验并触发升级检查
        UpgradeManager.Instance.AddExperience(xpPerAction);
    }
}