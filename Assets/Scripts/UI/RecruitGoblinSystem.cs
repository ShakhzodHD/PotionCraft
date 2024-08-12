using System.Diagnostics;
using UnityEngine;

public class RecruitGoblinSystem : MonoBehaviour
{
    [SerializeField] private int priceForWorkers;
    [SerializeField] private int priceForSeller;
    public void GoblinBuy(GameObject gameObject)
    {
        if (gameObject.tag == "GoblinWorker" && CurrencyManager.instance.CanAfford(priceForWorkers))
        {
            gameObject.SetActive(true);
            CurrencyManager.instance.SpendCurrency(priceForWorkers);
        }
        else
        {
            if (CurrencyManager.instance.CanAfford(priceForSeller))
            {
                gameObject.SetActive(true);
                CurrencyManager.instance.SpendCurrency(priceForSeller);
            }
        }
    }
}
