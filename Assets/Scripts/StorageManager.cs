using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StorageManager : MonoBehaviour
{
    public Image putProgressBar;
    public static float putDelay = 1.5f;
    public int maxCount;

    [SerializeField] private string namePotionForKeep;
    [SerializeField] private PickupObject playerPickup;

    private Coroutine putCoroutine;

    public ItemState statusItem;
    public enum ItemState
    {
        UnPickupable,
        Selling
    }

    protected virtual void Awake()
    {
        ShopperAI.numberStands--;
    }

    private void OnTriggerStay(Collider other)
    {
        bool isPlayer = other.CompareTag("Player");
        bool isAI = other.CompareTag("GoblinWorker");

        if (isPlayer || isAI)
        {
            foreach (Transform child in other.transform)
            {
                if (child.CompareTag(statusItem.ToString()) && child.name == namePotionForKeep)
                {
                    if (putCoroutine == null)
                    {
                        putCoroutine = StartCoroutine(PutObjWithDelay(child.gameObject, isPlayer));
                    }
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
                if (child.CompareTag(statusItem.ToString()) && child.name == namePotionForKeep)
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

    public virtual IEnumerator PutObjWithDelay(GameObject obj, bool isPlayer)
    {
        float timer = 0f;

        while (timer < putDelay)
        {
            timer += Time.deltaTime;
            UpdatePutProgressUI(timer / putDelay, isPlayer);
            yield return null;
        }

        PutObj(obj, isPlayer);
        putCoroutine = null;
    }

    public void PutObj(GameObject obj, bool isPlayer)
    {
        if (obj.CompareTag("Selling"))
        {
            obj.tag = "Sell";
            UpdatePutProgressUI(0, isPlayer);
        }

        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;

        if (isPlayer)
        {
            playerPickup.RemoveItem(obj);
        }
    }

    public virtual void CleaningAfterCraft()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("UnPickupable"))
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void UpdatePutProgressUI(float progress, bool isPlayer)
    {
        if (!isPlayer) return;
        if (putProgressBar != null)
        {
            putProgressBar.fillAmount = progress;
        }
    }
}
