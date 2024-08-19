using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private Text currencyText;

    private void OnEnable()
    {
        CurrencyManager.OnCurrencyChanged += UpdateCurrencyUI;
        UpdateCurrencyUI(CurrencyManager.instance.currencyAmount);
    }

    private void OnDisable()
    {
        CurrencyManager.OnCurrencyChanged -= UpdateCurrencyUI;
    }

    private void UpdateCurrencyUI(int newAmount)
    {
        currencyText.text = newAmount.ToString();
    }
}
