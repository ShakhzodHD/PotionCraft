using UnityEngine;

public class CapacityUpgrade : MonoBehaviour, IUpgrade
{
    [SerializeField] private PlayerPickup playerPickup;

    [SerializeField] private int basePrice;
    [SerializeField] private int baseInventoryLimit;
    [SerializeField] private int maxLevels = 3;

    [SerializeField] private int[] priceUpgradeForLevels;
    [SerializeField] private int[] inventoryLimitUpgradeForLevels;

    private int level;

    public int Level => level;
    public int BasePrice { get { return basePrice; } set { basePrice = value; } }
    private void Awake()
    {
        priceUpgradeForLevels[0] = basePrice;
        inventoryLimitUpgradeForLevels[0] = baseInventoryLimit;
    }

    public int GetPrice()
    {
        if (level >= maxLevels) return -1;
        int price = priceUpgradeForLevels[level];
        return price;
    }
    public void ApplyUpgrade()
    {
        if (level < maxLevels)
        {
            level++;
            Debug.Log("Улучшена грузоподьемность до уровня " + level);
            playerPickup.inventoryLimit = inventoryLimitUpgradeForLevels[level];
        }
        else
        {
            Debug.LogWarning("Достигнут максимальный уровень для этого улучшения!");
        }
    }
}
