﻿using UnityEngine;
using YG;

public class LoadManager : MonoBehaviour
{
    // аналогично когда нибудь зарефакторю

    public static LoadManager instance;

    [SerializeField] private BuyDecorSystem buyDecorSystem;
    [SerializeField] private RecruitGoblinSystem recruitGoblinSystem;
    [SerializeField] private GameObject[] plants;
    [SerializeField] private GameObject[] crafts;
    [SerializeField] private GameObject[] stands;

    [Header("Upgrades")]
    [SerializeField] private SpeedMovementUpgrade speedMovementUpgrade;
    [SerializeField] private CapacityUpgrade capacityUpgrade;
    [SerializeField] private SpeedActionUpgrade speedActionUpgrade;

    [SerializeField] private PickupObject playerPickup;

    [SerializeField] private BuyZoneSystem buyZoneSystem;
    [SerializeField] private ShopZone[] shopZones;

    [SerializeField] private DeviceTypeManager deviceType;

    [SerializeField] private UIButtonManager buttonManager;
    [SerializeField] private UIEnableCustomElement customElement;
    [SerializeField] private SpawnerBuyers buyers;

    [HideInInspector] public string _priceTextLanguage;
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
    private void OnShowWindowGame()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }
    public void GetLoad()
    {
        CurrencyManager.instance.LoadGold(YandexGame.savesData._goldAmount);
        LoadDecor();
        LoadRecruts();
        LoadObjects();

        LoadUpgrades(speedMovementUpgrade);
        LoadUpgrades(capacityUpgrade);
        LoadUpgrades(speedActionUpgrade);

        LoadBuyZone();
        LoadCurrentShopZoneValue();

        LoadStateTutoral();
        LoadUnlockEffecs();
        LoadStateWings();
        LoadStateDecorItem();
        LoadSpawnLevel();
        deviceType.DefineDivaceType();
    }

    public void LoadDecor()
    {
        GameObject[] items = buyDecorSystem.items;
        for (int i = 0; i < items.Length; i++)
        {
            if (YandexGame.savesData._openDecor[i] == true)
            {
                items[i].gameObject.SetActive(true);
            }
        }
    }
    public void LoadRecruts()
    {
        GameObject[] goblins = recruitGoblinSystem.goblins;
        for (int i = 0;i < goblins.Length; i++)
        {
            if (YandexGame.savesData._openRecruit[i] == true)
            {
                goblins[i].gameObject.SetActive(true);
            }
        }
    }
    public void LoadObjects()
    {
        for (int i = 0; i < plants.Length ; i++)
        {
            if (YandexGame.savesData._openPlants[i] == true)
            {
                plants[i].SetActive(true);
            }
        }
        for (int i = 0; i < crafts.Length; i++)
        {
            if (YandexGame.savesData._openCrafts[i] == true)
            {
                crafts[i].SetActive(true);
            }
        }
        for (int i = 0; i < stands.Length; i++)
        {
            if (YandexGame.savesData._openStands[i] == true)
            {
                stands[i].SetActive(true);
            }
        }
    }
    public void LoadUpgrades<T>(T upgrade) where T : IUpgrade
    {
        if (upgrade is SpeedMovementUpgrade speedMovementUpgrade)
        {
            int currentLevel = YandexGame.savesData._levelSpeedMovement;
            speedMovementUpgrade.Level = currentLevel;
            PlayerController.moveSpeed = speedMovementUpgrade.numberUpgradeForMovementSpeed[currentLevel];
        }
        if (upgrade is CapacityUpgrade capacity)
        {
            int currentLevel = YandexGame.savesData._levelCapacity;
            capacity.Level = currentLevel;
            playerPickup.inventoryLimit = capacity.inventoryLimitUpgradeForLevels[currentLevel];
        }
        if (upgrade is SpeedActionUpgrade speedAction)
        {
            int currentLevel = YandexGame.savesData._levelSpeedAction;
            speedAction.Level = currentLevel;
            playerPickup.pickupDelay = speedAction.delayUpgradeForLevels[currentLevel];
        }
    }
    public void LoadBuyZone()
    {
        buyZoneSystem.CountZone = YandexGame.savesData._countBuyZone;
    }
    private void LoadCurrentShopZoneValue()
    {
        for (int i = 0; i < shopZones.Length; i++)
        {
            if (shopZones[i] != null && shopZones[i].isActiveAndEnabled)
            {
                shopZones[i].fullPrice = YandexGame.savesData._valueCurrentBuyZone;
            }
        }
    }
    public void LoadStateTutoral()
    {
        Debug.Log("Загрузка "+YandexGame.savesData._completeTutorial);
        DialogueManager.instance.isCompete = YandexGame.savesData._completeTutorial;
    }
    public void LoadStateDecorItem()
    {
        PickLanguage();
        for (int i = 0; i < buyDecorSystem.buttons.Length; i++) 
        {
            if (YandexGame.savesData._openDecor[i] == true)
            {
                buyDecorSystem.SetSellText(i);
            }
        }
    }
    public void LoadUnlockEffecs()
    {
        bool[] unlockedEffects = YandexGame.savesData._buttonEffectsUnlocked;

        buttonManager.SetUnlockEffect = unlockedEffects;
    }
    private void LoadStateWings()
    {
        customElement.StateElement = YandexGame.savesData._isWings;
    }
    private void LoadSpawnLevel()
    {
        int count = YandexGame.savesData._levelSpawn;
        for (int i = 0; i < count; i++)
        {
            buyers.IncreaseValue();
        }
    }
    private void PickLanguage()
    {
        switch (YandexGame.lang)
        {
            case "ru":
                _priceTextLanguage = "Куплено!";
                break;
            case "en":
                _priceTextLanguage = "Purchased!";
                break;
            case "tr":
                _priceTextLanguage = "Satın alındı!";
                break;
            case "kk":
                _priceTextLanguage = "Сатып алынды!";
                break;
            default:
                _priceTextLanguage = "Purchased!";
                break;
        }
    }
    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
        YandexGame.onShowWindowGame += OnShowWindowGame; 
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
        YandexGame.onShowWindowGame -= OnShowWindowGame; 
    }
}
