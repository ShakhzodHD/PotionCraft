using UnityEngine;
using YG;

public class SaveManager : MonoBehaviour 
{
    // код полное дерьмо сделанное на коленьках, когда нибудь зарефакторю

    public static SaveManager instance;
    [SerializeField] private SpeedMovementUpgrade speedMovementUpgrade;
    [SerializeField] private CapacityUpgrade capacityUpgrade;
    [SerializeField] private SpeedActionUpgrade speedActionUpgrade;

    [SerializeField] private BuyZoneSystem buyZoneSystem;
    [SerializeField] private ShopZone[] shopZones;

    [SerializeField] private UIEnableCustomElement customElement;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            SaveGold();
        }
    }
    public void SaveGold()
    {
        YandexGame.savesData._goldAmount = CurrencyManager.instance.currencyAmount;
    }
    public void SaveDecor(int index)
    {
        YandexGame.savesData._openDecor[index] = true;
        SaveProgress();
    }
    public void SaveRecruit(int index)
    {
        YandexGame.savesData._openRecruit[index] = true;
        SaveProgress();
    }
    public void SaveObjects(GameObject item)
    {
        switch (item.name)
        {
            case "LivingTree":
                YandexGame.savesData._openPlants[0] = true;
                break;
            case "DracoWind":
                YandexGame.savesData._openPlants[1] = true;
                break;
            case "Inner":
                YandexGame.savesData._openPlants[2] = true;
                break;
            case "RegenCraft":
                YandexGame.savesData._openCrafts[0] = true;
                break;
            case "PowerCraft":
                YandexGame.savesData._openCrafts[1] = true;
                break;
            case "NecromancyCraft":
                YandexGame.savesData._openCrafts[2] = true;
                break;
            case "CurseCraft":
                YandexGame.savesData._openCrafts[3] = true;
                break;
            case "PowerSaleStand":
                YandexGame.savesData._openStands[1] = true;
                break;
            case "NecromancySaleStand":
                YandexGame.savesData._openStands[2] = true;
                break;
            case "CurseSaleStand":
                YandexGame.savesData._openStands[3] = true;
                break;
            default:
                break;
        }
        
        SaveProgress();
    }
    public void Save<T>(T upgrade) where T : IUpgrade
    {
        if (upgrade is SpeedMovementUpgrade speedMovementUpgrade)
        {
            YandexGame.savesData._levelSpeedMovement = speedMovementUpgrade.Level;
        }
        if (upgrade is CapacityUpgrade capacity)
        {
            YandexGame.savesData._levelCapacity = capacity.Level;
        }
        if (upgrade is SpeedActionUpgrade speedAction)
        {
            YandexGame.savesData._levelSpeedAction = speedAction.Level;
        }
    }
    public void SaveBuyZone()
    {
        YandexGame.savesData._countBuyZone = buyZoneSystem.CountZone;
        SaveProgress();
    }
    private void SaveAllShopZoneValue()
    {
        for (int i = 0; i < shopZones.Length; i++)
        {
            if (shopZones[i] != null && shopZones[i].isActiveAndEnabled)
            {
                YandexGame.savesData._valueCurrentBuyZone = shopZones[i].fullPrice;
            }
        }
        SaveProgress();
    }
    public void SaveStateTutoral(bool isComplete)
    {
        YandexGame.savesData._completeTutorial = isComplete;
        SaveProgress();
    }
    public void SaveUnlockEffecs(int index)
    {
        YandexGame.savesData._buttonEffectsUnlocked[index] = true;
        SaveProgress();
    }
    public void SaveActiveEffect(int index)
    {
        YandexGame.savesData._activeButtonIndex = index;
        SaveProgress();
    }
    public void SaveStateWings()
    {
        YandexGame.savesData._isWings = customElement.StateElement;
    }
    private void SaveProgress()
    {
        YandexGame.SaveProgress();
    }
    private void OnApplicationQuit()
    {
        SaveGold();
        Save(speedMovementUpgrade);
        Save(capacityUpgrade);
        Save(speedActionUpgrade);
        SaveBuyZone();
        SaveAllShopZoneValue();
        SaveStateWings();

        SaveProgress();
    }
}
