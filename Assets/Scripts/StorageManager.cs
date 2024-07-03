using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageManager : MonoBehaviour
{
    public Image putProgressBar;
    public float putDelay = 1f;
    public int maxCount;

    private Coroutine putCoroutine;

    public ItemState statusItem;
    public enum ItemState
    {
        UnPickupable,
        Selling
    }

    private void Start()
    {
        putProgressBar = FindObjectOfType<Image>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Transform child in other.transform)
            {
                if (child.tag == statusItem.ToString())
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
    private IEnumerator PutObjWithDelay(GameObject obj)
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

    private void UpdatePutProgressUI(float progress)
    {
        if (putProgressBar != null)
        {
            putProgressBar.fillAmount = progress;
        }
    }
}
