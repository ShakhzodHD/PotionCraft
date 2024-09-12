using UnityEngine;
using YG;

public class CapacityUpgrade : MonoBehaviour, IUpgrade
{
    [SerializeField] private PickupObject playerPickup;

    [SerializeField] private int basePrice;
    [SerializeField] private int baseInventoryLimit;
    [SerializeField] private int maxLevels = 3;

    [SerializeField] private int[] priceUpgradeForLevels;
    public int[] inventoryLimitUpgradeForLevels;

    private int level;

    public int Level
    {
        get => level;
        set => level = value;
    }
    public int MaxLevel => maxLevels;

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
    }
}
