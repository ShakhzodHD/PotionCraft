using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    public int maxCount;

    public ItemState statusItem;
    public enum ItemState
    {
        UnPickupable,
        Selling
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Transform child in other.transform)
            {
                if (child.tag == statusItem.ToString()) 
                {
                    PutObj(child.gameObject);
                }
            }
        }
    }

    public void PutObj(GameObject obj)
    {
        if (obj.tag == "Selling")
        {
            obj.tag = "Sell";     
        }
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
    }
    public virtual void CleaningAfterCraft()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.tag == "UnPickupable")
            {
                Destroy(child.gameObject);
            }
        }
    }
}
