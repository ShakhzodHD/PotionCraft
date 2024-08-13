using UnityEngine;

public class BuyDecorSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private int[] prices;
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
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }
}
