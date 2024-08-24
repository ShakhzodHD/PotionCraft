using System.Collections;
using UnityEngine;

public class StoragePlants : StorageManager
{
    private Coroutine putCoroutine;
    private int localCount = 0;

    public Resource.Ingredients statusIngredients;

    protected override void Awake()
    {
        // Переопределение метода Awake, если нужно
    }

    private void OnTriggerStay(Collider other)
    {
        bool isPlayer = other.CompareTag("Player");
        bool isAI = other.CompareTag("GoblinWorker");

        if (isPlayer || isAI)
        {
            foreach (Transform child in other.transform)
            {
                if (child.CompareTag(statusItem.ToString()) && child.name == statusIngredients.ToString() + "(Clone)")
                {
                    if (localCount < maxCount)
                    {
                        if (putCoroutine == null)
                        {
                            putCoroutine = StartCoroutine(PutObjWithDelay(child.gameObject, isPlayer));
                        }
                    }
                    break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("GoblinWorker"))
        {
            foreach (Transform child in other.transform)
            {
                if (child.CompareTag(statusItem.ToString()) && child.name == statusIngredients.ToString() + "(Clone)")
                {
                    if (putCoroutine != null)
                    {
                        StopCoroutine(putCoroutine);
                        putCoroutine = null;
                        UpdatePutProgressUI(0, false);
                    }
                }
            }
        }
    }

    public override IEnumerator PutObjWithDelay(GameObject obj, bool isPlayer)
    {
        float timer = 0f;

        while (timer < putDelay)
        {
            timer += Time.deltaTime;
            UpdatePutProgressUI(timer / putDelay, isPlayer);
            yield return null;
        }

        PutObj(obj, isPlayer);
        localCount++;
        putCoroutine = null;
    }

    public override void CleaningAfterCraft()
    {
        localCount = 0;
    }
}
