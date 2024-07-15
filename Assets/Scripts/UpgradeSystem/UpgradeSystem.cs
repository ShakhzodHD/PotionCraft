using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    UpgradeManager upgradeManager = new UpgradeManager();

    [Header("Ссылки на обьекты с улучшениями")]
    [SerializeField] private SpeedMovementUpgrade _speedMovementUpgrade;
    [SerializeField] private CapacityUpgrade _capacityUpgrade;
    [SerializeField] private SpeedActionUpgrade _speedActionUpgrade;

    [Header("Ссылки на текст в UI")]

    [Header("Цены")]
    [SerializeField] private Text _speedPriceText;
    [SerializeField] private Text _capacityPriceText;
    [SerializeField] private Text _actionPriceText;
    [Header("Названия")]
    [SerializeField] private Text nameSpeedMovementText;
    [SerializeField] private Text nameCapacityText;
    [SerializeField] private Text nameSpeedActionText;

    private void Start()
    {
        UpdateUI();
    }
    private void Upgrade(IUpgrade _upgrade)
    {
        upgradeManager.ApplyUpgrade(_upgrade);
        upgradeManager.GetUpgradePrice(_upgrade);
        UpdateUI();
    }
    public void UpgradeSpeedMovement() { Upgrade(_speedMovementUpgrade); }
    public void UpgradeCapacity() { Upgrade(_capacityUpgrade); }
    public void UpgradeSpeedAction() { Upgrade(_speedActionUpgrade); }
    private void UpdateUI()
    {
        UpdateUpgradePrice(_speedPriceText, _speedMovementUpgrade);
        UpdateUpgradePrice(_capacityPriceText, _capacityUpgrade);
        UpdateUpgradePrice(_actionPriceText, _speedActionUpgrade);

        UpdateNameText(nameSpeedMovementText, _speedMovementUpgrade, "Скорость передвижения ");
        UpdateNameText(nameCapacityText, _capacityUpgrade, "Грузоподъемность ");
        UpdateNameText(nameSpeedActionText, _speedActionUpgrade, "Скорость действия ");
    }
    private void UpdateUpgradePrice(Text text, IUpgrade _upgrade)
    {
        if (_upgrade != null)
        {
            int price = _upgrade.GetPrice();
            if (price == -1)
            {
                text.text = "Достигнут максимальный уровень";
            }
            else
            {
                text.text = "Цена: " + price;
            }
        }
    }
    private void UpdateNameText(Text text, IUpgrade upgrade, string name)
    {
        if (upgrade != null)
        {
            int level = upgrade.Level;
            if (level <= 0)
            {
                text.text = name;
            }
            else
            {
                text.text = name + level.ToString();
            }
        }
    }
}
