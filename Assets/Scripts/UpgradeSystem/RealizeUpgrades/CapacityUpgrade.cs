using UnityEngine;

public class CapacityUpgrade : MonoBehaviour, IUpgrade
{
    [SerializeField] private PlayerPickup playerPickup;

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
        if (level >= maxLevels) return -1; 
        int price = basePrice * (level + 1); // ждет обновлений реализации
        return price;
    }
    public void ApplyUpgrade()
    {
        if (level < maxLevels)
        {
            level++;
            Debug.Log("Улучшена грузоподьемность до уровня " + level);
            playerPickup.inventoryLimit = 0; // ждет обновлений реализации
        }
        else
        {
            Debug.LogWarning("Достигнут максимальный уровень для этого улучшения!");
        }
    }
}
