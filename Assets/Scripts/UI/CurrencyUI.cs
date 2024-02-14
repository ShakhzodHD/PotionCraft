using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private Text currencyText;
    private void Update()
    {
        UpdateCurrencyUI();
    }

    public void UpdateCurrencyUI()
    {
        int currencyAmount = CurrencyManager.instance.currencyAmount;
        currencyText.text = "Gold: " + currencyAmount.ToString();
    }
}
