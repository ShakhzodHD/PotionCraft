using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StorageManager : MonoBehaviour
{
    public Image putProgressBar;
    public float putDelay = 1f;
    public int maxCount;

    [SerializeField] private string namePotionForKeep;

    private Coroutine putCoroutine;

    public ItemState statusItem;
    public enum ItemState
    {
        UnPickupable,
        Selling
    }
    public virtual void Awake()
    {
        ShopperAI.numberStands--;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Transform child in other.transform)
            {
                if (child.tag == statusItem.ToString() && child.name == namePotionForKeep) 
                {
                    if (putCoroutine == null)
                    {
                        putCoroutine = StartCoroutine(PutObjWithDelay(child.gameObject));
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (putCoroutine != null)
            {
                StopCoroutine(putCoroutine);
                putCoroutine = null;
                UpdatePutProgressUI(0); 
            }
        }
    }
    public virtual IEnumerator PutObjWithDelay(GameObject obj)
    {
        float timer = 0f;

        while (timer < putDelay)
        {
            timer += Time.deltaTime;
            UpdatePutProgressUI(timer / putDelay);
            yield return null;
        }

        PutObj(obj);
        putCoroutine = null;
    }

    public void PutObj(GameObject obj)
    {
        if (obj.tag == "Selling")
        {
            obj.tag = "Sell";
            UpdatePutProgressUI(0);
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

    public void UpdatePutProgressUI(float progress)
    {
        if (putProgressBar != null)
        {
            putProgressBar.fillAmount = progress;
        }
    }
}
