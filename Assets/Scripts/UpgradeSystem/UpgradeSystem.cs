using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    UpgradeManager upgradeManager = new UpgradeManager();

    [Header("������ �� ������� � �����������")]
    [SerializeField] private SpeedMovementUpgrade _speedMovementUpgrade;
    [SerializeField] private CapacityUpgrade _capacityUpgrade;
    [SerializeField] private SpeedActionUpgrade _speedActionUpgrade;

    [Header("������ �� ����� � UI")]

    [Header("����")]
    [SerializeField] private Text _speedPriceText;
    [SerializeField] private Text _capacityPriceText;
    [SerializeField] private Text _actionPriceText;
    [Header("��������")]
    [SerializeField] private Text nameSpeedMovementText;
    [SerializeField] private Text nameCapacityText;
    [SerializeField] private Text nameSpeedActionText;

    private void Start()
    {
        UpdateUI();
    }
    private void Upgrade(IUpgrade _upgrade, bool isReward)
    {
        if (!isReward)
        {
            if (_upgrade.GetPrice() == -1) return;

            int upgradePrice = upgradeManager.GetUpgradePrice(_upgrade);

            if (CurrencyManager.instance.CanAfford(upgradePrice))
            {
                upgradeManager.ApplyUpgrade(_upgrade);
                CurrencyManager.instance.SpendCurrency(upgradePrice);
                PlayerSoundManager.manager.PlayUpgradeSound();
                UpdateUI();
            }
            else
            {
                PlayerSoundManager.manager.PlayCanselSound();
            }
        }
        else
        {
            upgradeManager.ApplyUpgrade(_upgrade);
            PlayerSoundManager.manager.PlayUpgradeSound();
            UpdateUI();
        }
        
    }
    public void UpgradeSpeedMovement(bool isReward) { Upgrade(_speedMovementUpgrade, isReward); }
    public void UpgradeCapacity(bool isReward) { Upgrade(_capacityUpgrade, isReward); }
    public void UpgradeSpeedAction(bool isReward) { Upgrade(_speedActionUpgrade, isReward); }
    private void UpdateUI()
    {
        UpdateUpgradePrice(_speedPriceText, _speedMovementUpgrade);
        UpdateUpgradePrice(_capacityPriceText, _capacityUpgrade);
        UpdateUpgradePrice(_actionPriceText, _speedActionUpgrade);

        UpdateNameText(nameSpeedMovementText, _speedMovementUpgrade, "�������� ������������ ");
        UpdateNameText(nameCapacityText, _capacityUpgrade, "���������������� ");
        UpdateNameText(nameSpeedActionText, _speedActionUpgrade, "�������� �������� ");
    }
    private void UpdateUpgradePrice(Text text, IUpgrade _upgrade)
    {
        if (_upgrade != null)
        {
            int price = _upgrade.GetPrice();
            if (price == -1)
            {
                text.text = "��������� ������������ �������";
            }
            else
            {
                text.text = "����: " + price;
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
