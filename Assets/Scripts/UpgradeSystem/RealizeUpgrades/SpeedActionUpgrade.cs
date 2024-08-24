using UnityEngine;

public class SpeedActionUpgrade : MonoBehaviour, IUpgrade
{
    [SerializeField] private PickupObject playerPickup;

    [SerializeField] private int basePrice;
    [SerializeField] private float baseDelay;
    [SerializeField] private int maxLevels = 3;

    [SerializeField] private int[] priceUpgradeForLevels;
    [SerializeField] private float[] delayUpgradeForLevels;

    private int level;

    public int Level => level;

    public int BasePrice { get { return basePrice; } set { basePrice = value; } }
    private void Awake()
    {
        priceUpgradeForLevels[0] = basePrice;
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
            Debug.Log("Улучшена скорость крафта до уровня " + level);
            playerPickup.pickupDelay = delayUpgradeForLevels[level];
        }
        else
        {
            Debug.LogWarning("Достигнут максимальный уровень для этого улучшения!");
        }
    }
}
