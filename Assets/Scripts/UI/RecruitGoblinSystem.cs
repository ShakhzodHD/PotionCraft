using UnityEngine;
using UnityEngine.UI;

public class RecruitGoblinSystem : MonoBehaviour
{
    public GameObject[] goblins;

    [SerializeField] private int[] pricesGoblins;
    [SerializeField] private Button[] buttons;
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
        else
        {
            PlayerSoundManager.manager.PlayCanselSound();
        }
    }
    private void RemoveButtonInteractable(int index)
    {
        buttons[index].interactable = false;
    }
}
