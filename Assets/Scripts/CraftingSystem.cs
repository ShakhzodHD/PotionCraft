using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public itemCraft typeItem;
    [SerializeField] private GameObject craftObjectModel;
    [SerializeField] private StorageManager storageManager;

    public int requiredItemCount;
    [SerializeField] private bool isCrafting = false;

    public enum itemCraft
    {
        Potion,
        Huy
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
            GameObject craftedObj = Instantiate(craftObjectModel, transform.position, Quaternion.identity);
            craftedObj.transform.SetParent(transform);
            craftObjectModel.tag = "Selling";
            isCrafting = false;
        }
    }
}
