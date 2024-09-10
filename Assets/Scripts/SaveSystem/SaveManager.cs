using UnityEngine;
using YG;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

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
        YandexGame.savesData.goldAmount = CurrencyManager.instance.currencyAmount;
    }
    private void OnApplicationQuit()
    {
        SaveGold();
        SaveProgress();
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

    private void SaveProgress()
    {
        YandexGame.SaveProgress();
    }
}
