using UnityEngine;

public class SpeedMovementUpgrade : MonoBehaviour, IUpgrade
{
    [SerializeField] private int basePrice;
    [SerializeField] private int maxLevels = 3;
    private int level;

    public int Level => level;
    public int BasePrice { get { return basePrice; } set { basePrice = value; } }
    public SpeedMovementUpgrade(int basePrice, int maxLevels)
    {
        this.basePrice = basePrice;
        this.maxLevels = maxLevels;
        this.level = 0;
    }

    public  int GetPrice()
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
            Debug.Log("Улучшена скорость передвижения до уровня " + level);
            // Дополнительная логика для мувспида
        }
        else
        {
            Debug.LogWarning("Достигнут максимальный уровень для этого улучшения!");
        }
    }
}
