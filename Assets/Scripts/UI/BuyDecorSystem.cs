using UnityEngine;
using UnityEngine.UI;
using YG;

public class BuyDecorSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private int[] prices;

    [SerializeField] private Text[] pricesText;

    private string _priceTextLanguage;
    private void Start()
    {
        PickLanguage();

        for (int i = 0; i < prices.Length; i++)
        {
            pricesText[i].text = prices[i].ToString();
        }
    }
    public void BuyItem(int index)
    {
        if (items.Length != prices.Length)
        {
            Debug.LogError("The number of objects does not match the number of prices.");
            return;
        }
        if (CurrencyManager.instance.CanAfford(prices[index]))
        {
            items[index].SetActive(true);
            CurrencyManager.instance.SpendCurrency(prices[index]);
            PlayerSoundManager.manager.PlayBuyDecorSound();
            pricesText[index].text = _priceTextLanguage;
        }
        else
        {
            PlayerSoundManager.manager.PlayCanselSound();
            Debug.Log("Not enough gold");
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
}
