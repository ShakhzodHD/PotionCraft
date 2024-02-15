using UnityEngine;

public class ShopZone : MonoBehaviour
{
    [Header("Parameters about the object being purchased")]
    [SerializeField] GameObject itemToBuy;
    [SerializeField] private int fullPrice;
    [SerializeField] private int itemCost;

    [SerializeField] private float framesPerUpdate = 3;
    private float frameCount = 0;
    private void OnTriggerStay(Collider other)
    {
        frameCount++;
        if (frameCount >= framesPerUpdate)
        {
            if (other.CompareTag("Player"))
            {
                if (CurrencyManager.instance.CanAfford(itemCost))
                {
                    CurrencyManager.instance.SpendCurrency(itemCost);
                    fullPrice--;
                }
                else
                {
                    Debug.Log("Not enough currency to buy the item!");
                }
            }
            frameCount = 0;
        }
    }
    private void Update()
    {
        BuyItem();
    }
    private void BuyItem()
    {
        if (fullPrice <= 0)
        {
            itemToBuy.SetActive(true);
            Destroy(gameObject);
        }
    }
}
