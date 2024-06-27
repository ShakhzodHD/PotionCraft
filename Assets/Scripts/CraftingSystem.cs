using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private GameObject craftObjectPrefab;
    private StorageManager storageManager;

    public int requiredItemCount;
    [SerializeField] private bool isCrafting = false;

    private void Start()
    {
        storageManager = GetComponent<StorageManager>();
    }

    private void Update()
    {
        if (!isCrafting && storageManager.currentItem >= requiredItemCount)
        {
            isCrafting = true;
            CraftItem();
        }
    }
    private void CraftItem()
    {
        if (storageManager.currentItem >= requiredItemCount)
        {
            storageManager.TakeItemForCraft();
            GameObject craftedObj = Instantiate(craftObjectPrefab, transform.position, Quaternion.identity);
            craftedObj.transform.SetParent(transform);
            craftObjectPrefab.tag = "Selling";
            isCrafting = false;
        }
    }
}
