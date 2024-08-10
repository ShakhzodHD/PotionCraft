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
    private void Upgrade(IUpgrade _upgrade)
    {
        int upgradePrice = upgradeManager.GetUpgradePrice(_upgrade);

        if (CurrencyManager.instance.CanAfford(upgradePrice))
        {
            upgradeManager.ApplyUpgrade(_upgrade);
            CurrencyManager.instance.SpendCurrency(upgradePrice);
            UpdateUI();
        }
        else
        {
            Debug.LogWarning("������������ ������ ��� ��������!");
        }
    }
    public void UpgradeSpeedMovement() { Upgrade(_speedMovementUpgrade); }
    public void UpgradeCapacity() { Upgrade(_capacityUpgrade); }
    public void UpgradeSpeedAction() { Upgrade(_speedActionUpgrade); }
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
