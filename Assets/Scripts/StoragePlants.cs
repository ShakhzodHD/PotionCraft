using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePlants : StorageManager
{
    public Resource.Ingredients statusIngredients;
    private int localCount = 0;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Transform child in other.transform)
            {
                if (child.tag == statusItem.ToString() && child.name == statusIngredients.ToString() + "(Clone)")
                {
                    if (localCount < maxCount)
                    {
                        PutObj(child.gameObject);
                        localCount++;  
                    }
                    else
                    {
                        Debug.Log("Достигнут лимит хранилища растений!");
                        // Здесь можно добавить обработку, если хотите предпринять какие-то действия при достижении лимита
                    }
                    break; 
                }
            }
        }
    }
    public override void CleaningAfterCraft()
    {
        localCount = 0;  
    }
}
