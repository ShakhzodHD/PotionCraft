using UnityEngine;

public class SpeedActionUpgrade : MonoBehaviour, IUpgrade
{
    [SerializeField] private int basePrice;
    [SerializeField] private int maxLevels = 3;
    private int level;

    public int Level => level;

    public int BasePrice { get { return basePrice; } set { basePrice = value; } }
    public void Initialize(int basePrice, int maxLevels)
    {
        this.basePrice = basePrice;
        this.maxLevels = maxLevels;
        this.level = 0;
    }

    public int GetPrice()
    {
        int price = basePrice * (level + 1);
        Debug.Log(price);
        return price;
    }

    public void ApplyUpgrade()
    {
        if (level < maxLevels)
        {
            level++;
            Debug.Log("Улучшена скорость крафта до уровня " + level);
            // Дополнительная логика скорости крафта
        }
        else
        {
            Debug.LogWarning("Достигнут максимальный уровень для этого улучшения!");
        }
    }
}
