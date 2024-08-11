﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private GoblinWorkerAI worker;

    [SerializeField] private GameObject craftObjectPrefab;
    [SerializeField] private bool isUseCrystal = false;
    [SerializeField] private bool isCrafting = false;

    [Header("Offset Position and Rotation Craft Obj")]
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private float offsetZ;
    [SerializeField] private Vector3 rotationOffset = new Vector3(90, 0, 0);


    private StoragePlants plants;
    private StorageCrystal crystals;

    public int requiredItemCount;
    public Resource.Ingredients requiredObj;

    public int requiredSecondItemCount;
    public Resource.Ingredients secondRequiredObj;

    private void Start()
    {
        plants = GetComponent<StoragePlants>();
        crystals = GetComponent<StorageCrystal>();

        if (plants == null)
        {
            Debug.LogError("Компонент StoragePlants отсутствует.");
        }

        if (isUseCrystal && crystals == null)
        {
            Debug.LogError("Компонент StorageCrystal отсутствует, но isUseCrystal установлено в true.");
        }
    }

    private void Update()
    {
        if (!isCrafting)
        {
            int firstIngredientCount = CountChildrenWithName(requiredObj.ToString() + "(Clone)", plants?.transform);
            int secondIngredientCount = isUseCrystal ? CountChildrenWithName(secondRequiredObj.ToString() + "(Clone)", crystals?.transform) : 0;

            if (firstIngredientCount >= requiredItemCount && (!isUseCrystal || secondIngredientCount >= requiredSecondItemCount))
            {
                StartCoroutine(CraftItem());
            }
        }
    }

    private IEnumerator CraftItem()
    {
        isCrafting = true;

        RemoveItems(requiredObj.ToString() + "(Clone)", requiredItemCount, plants.transform);
        if (isUseCrystal)
        {
            RemoveItems(secondRequiredObj.ToString() + "(Clone)", requiredSecondItemCount, crystals.transform);
        }

        Vector3 spawnPos = gameObject.transform.position;
        spawnPos.x += offsetX;
        spawnPos.y += offsetY;
        spawnPos.z += offsetZ;

        Quaternion rotation = Quaternion.Euler(rotationOffset);

        GameObject craftedObj = Instantiate(craftObjectPrefab, spawnPos, Quaternion.identity * rotation);
        craftedObj.transform.SetParent(transform);
        craftedObj.tag = "Selling";

        plants.CleaningAfterCraft();
        if (isUseCrystal)
        {
            crystals.CleaningAfterCraft();
        }

        yield return new WaitForSeconds(1f);

        isCrafting = false;

        worker.SetIsCraft(true);
        if (isUseCrystal == true)
        {
            worker.SetNeedsCrystalForCraft(true);
        }     
    }

    private int CountChildrenWithName(string name, Transform parent)
    {
        if (parent == null) return 0;

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
        if (parent == null) return;

        int removedCount = 0;
        List<GameObject> toRemove = new List<GameObject>();
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                toRemove.Add(child.gameObject);
                removedCount++;
                if (removedCount >= count)
                {
                    break;
                }
            }
        }

        foreach (GameObject obj in toRemove)
        {
            Destroy(obj);
        }
    }
}
