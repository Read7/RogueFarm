using UnityEngine;
using UnityEngine.UI;

public class FieldHealthManager : MonoBehaviour
{
    public static FieldHealthManager Instance { get; private set; }

    [Header("血量设定")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float regenRate = 2f;       // 每秒回复量
    public float regenDelay = 5f;      // 受伤后多少秒开始回复

    [Header("UI 引用")]
    public Slider healthBar;           // 拖入你的 UI Slider

    private float _lastDamageTime;     // 记录上次受伤时间

    private void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        HandleRegeneration();
        UpdateUI();
    }

    /// <summary>
    /// 受到灾害伤害
    /// </summary>
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        _lastDamageTime = Time.time; // 更新最后受伤时间

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameManager.Instance.EndGame();
        }
    }

    /// <summary>
    /// 自然恢复逻辑
    /// </summary>
    private void HandleRegeneration()
    {
        // 逻辑：如果当前时间 - 最后受伤时间 > 延迟时间，且血量未满
        if (Time.time - _lastDamageTime > regenDelay && currentHealth < maxHealth)
        {
            // 这里可以加入“是否有存活作物”的判定
            currentHealth += regenRate * Time.deltaTime;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }
    }

    private void UpdateUI()
    {
        if (healthBar != null) healthBar.value = currentHealth / maxHealth;
    }
}