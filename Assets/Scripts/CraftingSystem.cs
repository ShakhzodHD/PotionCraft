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
        if (storageManager.currentItem >= requiredItemCount) isCrafting = true;
        if (isCrafting == true)
        {
            CraftItem();
            isCrafting = false;
        }
    }
    private void CraftItem()
    {
        if (storageManager.currentItem >= requiredItemCount)
        {
            storageManager.TakeItemForCraft();
            Instantiate(craftObjectModel);
            craftObjectModel.tag = "Selling";
        }
    }
}
