﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public virtual class StorageManager : MonoBehaviour
{
    public byte maxCountItem;
    public int currentItem = 0;

    [SerializeField] private ItemState statusItem;
    [SerializeField] private CraftingSystem crafting;
    private enum ItemState
    {
        UnPickupable,
        Selling
    }
    private void Start()
    {
        crafting = GetComponent<CraftingSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.transform.childCount > maxCountItem - 1) return;
            foreach (Transform child in other.transform)
            {
                if (child.tag == statusItem.ToString() && child.name == crafting.requiredObj.ToString() + ("(Clone)")) 
                {
                    PutObj(child.gameObject);
                }
            }
        }
    }

    private void PutObj(GameObject objToPickUp)
    {
        objToPickUp.transform.SetParent(transform);
        currentItem++;
        if (objToPickUp.tag == "Selling")
        {        
            objToPickUp.tag = "Sell";     
        }
    }

    public void TakeItemForCraft()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.tag == "UnPickupable")
            {
                Destroy(child.gameObject);
                currentItem--;
            }
        }
    }
}
