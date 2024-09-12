using UnityEngine;
using UnityEngine.UI;

public class RecruitGoblinSystem : MonoBehaviour
{
    public GameObject[] goblins;
    public Button[] buttons;

    [SerializeField] private int[] pricesGoblins;
    public void BuyGoblin(int index)
    {
        if (index < 0 || index >= goblins.Length) return;
        if (CurrencyManager.instance.CanAfford(pricesGoblins[index]))
        {
            goblins[index].gameObject.SetActive(true);
            CurrencyManager.instance.SpendCurrency(pricesGoblins[index]);

            RemoveButtonInteractable(index);
            SaveManager.instance.SaveRecruit(index);
        }
    }
    public void RemoveButtonInteractable(int index)
    {
        buttons[index].interactable = false;
    }
}
