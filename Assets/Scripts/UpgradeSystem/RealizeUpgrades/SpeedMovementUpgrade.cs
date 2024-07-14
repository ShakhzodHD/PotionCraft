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
        if (level >= maxLevels) return -1;
        int price = basePrice * (level + 1); // ждет обновлений реализации
        return price;
    }

    public void ApplyUpgrade()
    {
        if (level < maxLevels)
        {
            level++;
            Debug.Log("Улучшена скорость передвижения до уровня " + level);
            PlayerController.moveSpeed = 0;
            StorageManager.putDelay = 0;
        }
        else
        {
            Debug.LogWarning("Достигнут максимальный уровень для этого улучшения!");
        }
    }
}
