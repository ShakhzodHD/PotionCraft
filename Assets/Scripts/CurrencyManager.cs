using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    [SerializeField] private int startingGold;

    private int _currencyAmount = 0;
    public int currencyAmount
    {
        get => _currencyAmount;
        private set
        {
            _currencyAmount = value;
            OnCurrencyChanged?.Invoke(_currencyAmount);
        }
    }

    public static event System.Action<int> OnCurrencyChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            currencyAmount = startingGold;
            DontDestroyOnLoad(gameObject);
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
