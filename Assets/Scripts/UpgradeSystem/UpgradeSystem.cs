using UnityEngine;
using UnityEngine.UI;
using YG;

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

    private string _speedMovementText;
    private string _capacityText;
    private string _speedActionText;

    private string _priceText;
    private string _finishText;

    private void Start()
    {
        UpdateUI();
        UpdateLanguage();
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
    public void UpdateUI()
    {
        UpdateLanguage();

        UpdateUpgradePrice(_speedPriceText, _speedMovementUpgrade);
        UpdateUpgradePrice(_capacityPriceText, _capacityUpgrade);
        UpdateUpgradePrice(_actionPriceText, _speedActionUpgrade);

        UpdateNameText(nameSpeedMovementText, _speedMovementUpgrade, _speedMovementText);
        UpdateNameText(nameCapacityText, _capacityUpgrade, _capacityText);
        UpdateNameText(nameSpeedActionText, _speedActionUpgrade, _speedActionText);
    }
    private void UpdateUpgradePrice(Text text, IUpgrade _upgrade)
    {
        if (_upgrade != null)
        {
            int price = _upgrade.GetPrice();
            if (price == -1)
            {
                text.text = _finishText;
            }
            else
            {
                text.text = _priceText + price;
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
    private void UpdateLanguage()
    {
        switch (YandexGame.lang)
        {
            case "ru":
                _speedMovementText = "Скорость передвижение ";
                _capacityText = "Грудоподьемность ";
                _speedActionText = "Скорость сбора ";

                _priceText = "Цена: ";
                _finishText = "Достигнут максимум";
                break;
            case "en":
                _speedMovementText = "Movement Speed ";
                _capacityText = "Carrying Capacity " ;
                _speedActionText = "Harvesting Speed ";

                _priceText = "Price: ";
                _finishText = "Maximum reached";
                break;
            case "tr":
                _speedMovementText = "Hareket Hızı ";
                _capacityText = "Yük Kapasitesi ";
                _speedActionText = "Hasat Hızı ";

                _priceText = "Fiyat: ";
                _finishText = "Maksimuma ulaşıldı";
                break;
            case "kk":
                _speedMovementText = "Қозғалу жылдамдығы ";
                _capacityText = "Жүк көтергіштік ";
                _speedActionText = "Жинау жылдамдығы ";

                _priceText = "Баға: ";
                _finishText = "Шекке жетті";
                break;
            default:
                _speedMovementText = "Movement Speed ";
                _capacityText = "Carrying Capacity ";
                _speedActionText = "Harvesting Speed ";

                _priceText = "Price: ";
                _finishText = "Maximum level reached";
                break;
        }
    }
}
