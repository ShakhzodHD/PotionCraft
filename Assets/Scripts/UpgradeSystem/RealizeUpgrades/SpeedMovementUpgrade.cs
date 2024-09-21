using UnityEngine;
using UnityEngine.UIElements;
using YG;

public class SpeedMovementUpgrade : MonoBehaviour, IUpgrade
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private int basePrice;
    [SerializeField] private int maxLevels = 3;

    [SerializeField] private int[] priceUpgradeForLevels;
    public float[] numberUpgradeForMovementSpeed;

    private int level;

    public int Level
    {
        get => level;
        set => level = value; 
    }
    public int BasePrice { get { return basePrice; } set { basePrice = value; } }
    public SpeedMovementUpgrade(int basePrice, int maxLevels)
    {
        this.basePrice = basePrice;
        this.maxLevels = maxLevels;
        this.level = 0;
    }
    private void Awake()
    {
        priceUpgradeForLevels[0] = basePrice;
        numberUpgradeForMovementSpeed[0] = baseSpeed;
    }
    public  int GetPrice()
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
            PlayerController.moveSpeed = numberUpgradeForMovementSpeed[level];
        }
        else
        {
            Debug.LogWarning("Достигнут максимальный уровень для этого улучшения!");
        }
    }
}
