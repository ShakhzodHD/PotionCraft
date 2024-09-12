using UnityEngine;
using UnityEngine.UI;
using YG;

public class BuyDecorSystem : MonoBehaviour
{
    public GameObject[] items;

    [SerializeField] private int[] prices;

    public Text[] pricesText;
    public Button[] buttons;
    private void Start()
    {
        for (int i = 0; i < prices.Length; i++)
        {
            if (YandexGame.savesData._openDecor[i] == false)
            {
                pricesText[i].text = prices[i].ToString();
            }
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
            SetSellText(index);

            SaveManager.instance.SaveDecor(index);
        }
        else
        {
            PlayerSoundManager.manager.PlayCanselSound();
            Debug.Log("Not enough gold");
        }
    }
    public void SetSellText(int index)
    {
        pricesText[index].text = LoadManager.instance._priceTextLanguage;
        RemoveButtonInteractable(index);
    }
    private void RemoveButtonInteractable(int index)
    {
        buttons[index].interactable = false;
    }
}
