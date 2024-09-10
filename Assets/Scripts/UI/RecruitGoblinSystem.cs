using UnityEngine;

public class RecruitGoblinSystem : MonoBehaviour
{
    public GameObject[] goblins;

    [SerializeField] private int[] pricesGoblins;
    public void BuyGoblin(int index)
    {
        if (index < 0 || index >= goblins.Length) return;
        if (CurrencyManager.instance.CanAfford(pricesGoblins[index]))
        {
            goblins[index].gameObject.SetActive(true);
            CurrencyManager.instance.SpendCurrency(pricesGoblins[index]);

            SaveManager.instance.SaveRecruit(index);
        }
    }
}
