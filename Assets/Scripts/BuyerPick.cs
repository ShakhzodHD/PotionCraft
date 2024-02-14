using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerPick : MonoBehaviour
{
    public bool isItemInPurchase = false;
    private GameObject item;
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("ПОИСК ИДЕТ");
        item = other.gameObject;
        if (item.tag == "Sell")
        {
            TakeForBuy();
        }
    }

    private void TakeForBuy()
    {
        isItemInPurchase = true;
        item.transform.SetParent(transform);
    }

}
