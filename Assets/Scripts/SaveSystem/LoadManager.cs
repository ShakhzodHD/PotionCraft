using UnityEngine;
using YG;

public class LoadManager : MonoBehaviour
{
    public static LoadManager instance;

    [SerializeField] private BuyDecorSystem buyDecorSystem;
    [SerializeField] private RecruitGoblinSystem recruitGoblinSystem;
    [SerializeField] private GameObject[] plants;
    [SerializeField] private GameObject[] crafts;
    [SerializeField] private GameObject[] stands;

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
            GetLoad();
        }
    }
    public void GetLoad()
    {
        CurrencyManager.instance.LoadGold(YandexGame.savesData.goldAmount);
        LoadDecor();
        LoadRecruts();
        LoadObjects();
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
    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }
}
