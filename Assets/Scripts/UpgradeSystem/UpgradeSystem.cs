using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UpgradeSystem : MonoBehaviour
{
    UpgradeManager upgradeManager = new UpgradeManager();

    [Header("Ссылки на обьекты с улучшениями")]
    [SerializeField] private SpeedMovementUpgrade _speedMovementUpgrade;
    [SerializeField] private CapacityUpgrade _capacityUpgrade;
    [SerializeField] private SpeedActionUpgrade _speedActionUpgrade;

    [Header("Ссылки на текст в UI")]
    [SerializeField] private Text _speedPriceText;
    [SerializeField] private Text _capacityPriceText;
    [SerializeField] private Text _actionPriceText;

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
}
