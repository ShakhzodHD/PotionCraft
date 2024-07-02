using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private GameObject craftObjectPrefab;
    [SerializeField] private bool isUseCrystal = false;
    [SerializeField] private bool isCrafting = false;

    private StoragePlants plants;
    private StorageCrystal crystals; // Изменение на StorageCrystal

    public int requiredItemCount;
    public Resource.Ingredients requiredObj;

    public int requiredSecondItemCount;
    public Resource.Ingredients secondRequiredObj;

    private void Start()
    {
        plants = GetComponent<StoragePlants>();
        crystals = GetComponent<StorageCrystal>(); // Получаем ссылку на StorageCrystal
    }

    private void Update()
    {
        if (!isCrafting)
        {
            int firstIngredientCount = CountChildrenWithName(requiredObj.ToString() + "(Clone)", plants.transform);
            int secondIngredientCount = isUseCrystal ? CountChildrenWithName(secondRequiredObj.ToString() + "(Clone)", crystals.transform) : 0;

            if (firstIngredientCount >= requiredItemCount && (!isUseCrystal || secondIngredientCount >= requiredSecondItemCount))
            {
                isCrafting = true;
                CraftItem();
            }
        }
    }

    private void CraftItem()
    {
        RemoveItems(requiredObj.ToString() + "(Clone)", requiredItemCount, plants.transform);
        if (isUseCrystal)
        {
            RemoveItems(secondRequiredObj.ToString() + "(Clone)", requiredSecondItemCount, crystals.transform);
        }

        GameObject craftedObj = Instantiate(craftObjectPrefab, transform.position, Quaternion.identity);
        craftedObj.transform.SetParent(transform);
        craftedObj.tag = "Selling";

        plants.CleaningAfterCraft();
        crystals.CleaningAfterCraft();

        isCrafting = false;
    }

    private int CountChildrenWithName(string name, Transform parent)
    {
        int count = 0;
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                count++;
            }
        }
        return count;
    }

    private void RemoveItems(string name, int count, Transform parent)
    {
        int removedCount = 0;
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                Destroy(child.gameObject);
                removedCount++;
                if (removedCount >= count)
                {
                    break;
                }
            }
        }
    }
}
