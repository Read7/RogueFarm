using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimalAI
{
    void AttackCrop(FarmTile targetTile);
    void Flee();
}

public class WildElephant : MonoBehaviour, IAnimalAI
{
    public void AttackCrop(FarmTile targetTile)
    {
        // 野象的行为是范围破坏：直接攻击全局田地血条
        Debug.Log("野象正在践踏田地！");
        FieldHealthManager.Instance.TakeDamage(15f);
    }

    public void Flee() => Destroy(gameObject);

    // 沟通逻辑示例
    public void OnCommunicate()
    {
        Debug.Log("通过自然亲和力，你引导野象绕路了。");
        Flee();
    }
}