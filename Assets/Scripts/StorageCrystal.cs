using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StorageCrystal : StorageManager
{
    private Coroutine putCoroutine;
    private int localCount = 0;

    public Resource.Ingredients statusIngredients;
    public override void Awake()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Transform child in other.transform)
            {
                if (child.tag == statusItem.ToString() && child.name == statusIngredients.ToString() + "(Clone)")
                {
                    if (localCount < maxCount)
                    {
                        if (putCoroutine == null)
                        {
                            putCoroutine = StartCoroutine(PutObjWithDelay(child.gameObject));
                        }
                    }
                    else
                    {
                        Debug.Log("Достигнут лимит хранилища кристаллов!");
                    }
                    break;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Transform child in other.transform)
            {
                if (child.tag == statusItem.ToString() && child.name == statusIngredients.ToString() + "(Clone)")
                {
                    if (putCoroutine != null)
                    {
                        StopCoroutine(putCoroutine);
                        putCoroutine = null;
                        UpdatePutProgressUI(0); 
                    }
                }
            }
        }
    }

    public override IEnumerator PutObjWithDelay(GameObject obj)
    {
        float timer = 0f;

        while (timer < putDelay)
        {
            timer += Time.deltaTime;
            UpdatePutProgressUI(timer / putDelay);
            yield return null;
        }

        PutObj(obj);
        localCount++;
        putCoroutine = null;
    }

    public override void CleaningAfterCraft()
    {
        localCount = 0;
    }
}
