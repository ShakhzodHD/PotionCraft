using UnityEngine;

public class BuyerPick : MonoBehaviour
{
    [SerializeField] private ShopperAI shopper;

    public bool isItemInPurchase = false;
    private GameObject item;
    private void Start()
    {
        shopper = GetComponent<ShopperAI>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (isItemInPurchase) return;

        item = other.gameObject;
        if (item.CompareTag("Sell") && item.name == shopper.currentNeed + "(Clone)")
        {
            TakeForBuy();
        }
    }

    private void TakeForBuy()
    {
        isItemInPurchase = true;
        item.transform.SetParent(transform);
        item.tag = "Untagged";
    }

}
