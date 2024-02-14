using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public int currencyAmount = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddCurrency(int amount)
    {
        currencyAmount += amount;
    }
    public bool CanAfford(int amount)
    {
        return currencyAmount >= amount;
    }
    public void SpendCurrency(int amount)
    {
        if (CanAfford(amount))
        {
            currencyAmount -= amount;
        }
        else
        {
            Debug.LogWarning("Not enough currency!");
        }
    }
}
