using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PickupObject : MonoBehaviour
{
    public int inventoryLimit;
    public float pickupDelay;

    [SerializeField] private bool isPlayer;
    [SerializeField] private Image pickupProgressBar;
    [SerializeField] private Vector3 basePosition;

    private List<GameObject> items = new List<GameObject>();

    private int currentCountItem = 0;
    private bool isPickingUp = false;
    private Coroutine pickupCoroutine;
    private UnityEvent<GameObject, PickupObject> OnResourceTaked = new UnityEvent<GameObject, PickupObject>();

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Pickupable") && !other.CompareTag("Selling")) return;
        int temp = gameObject.transform.childCount;
        if (temp > inventoryLimit || isPickingUp) return;

        pickupCoroutine = StartCoroutine(PickUpObj(other.gameObject));
    }

    private void OnTriggerExit(Collider other)
    {
        if (pickupCoroutine != null)
        {
            StopCoroutine(pickupCoroutine);
            ResetPickupProgressUI();
            isPickingUp = false;
        }
    }

    private IEnumerator PickUpObj(GameObject objToPickUp)
    {
        isPickingUp = true;
        float timer = 0f;
        MeshRenderer meshRenderer = objToPickUp.GetComponent<MeshRenderer>();

        while (timer < pickupDelay)
        {
            timer += Time.deltaTime;
            UpdatePickupProgressUI(timer / pickupDelay);
            yield return null;
        }

        if (objToPickUp.CompareTag("Pickupable"))
        {
            GameObject plant = objToPickUp.transform.parent?.gameObject;
            if (plant != null)
            {
                OnResourceTaked?.Invoke(plant, this);  
            }
            objToPickUp.tag = "UnPickupable";
            SetTransfromObj(objToPickUp);
            meshRenderer.enabled = true;

            objToPickUp.GetComponent<PlaySpawnEffect>().Play();
            objToPickUp.GetComponent<PlaySpawnSound>().Play();
        }
        else if (objToPickUp.CompareTag("Selling"))
        {
            SetTransfromObj(objToPickUp);

            if (ActivatorDialogue.instanse != null)
            {
                ActivatorDialogue.instanse.Craft();
            }
        }

        ResetPickupProgressUI();
        isPickingUp = false;
    }
    private void SetTransfromObj(GameObject obj)
    {
        if (isPlayer == true)
        {
            obj.transform.SetParent(transform);

            Vector3 newPosition = basePosition + new Vector3(0, 0.8f * items.Count, 0);

            obj.transform.localPosition = newPosition;

            items.Add(obj);
            currentCountItem = items.Count;
        }
        else
        {
            obj.transform.SetParent(transform);
            obj.transform.localPosition = basePosition;
        }
    }
    public void RemoveItem(GameObject obj)
    {
        if (items.Contains(obj))
        {
            items.Remove(obj);
            currentCountItem = items.Count;

            for (int i = 0; i < items.Count; i++)
            {
                Vector3 newPosition = basePosition + new Vector3(0, 0.8f * i, 0);
                items[i].transform.localPosition = newPosition;
            }
        }
    }

    public void SubscribeResourceTaked(UnityAction<GameObject, PickupObject> listener)
    {
        OnResourceTaked.AddListener(listener);
    }

    public void UnsubscribeResourceTaked(UnityAction<GameObject, PickupObject> listener)
    {
        OnResourceTaked.RemoveListener(listener);
    }

    private void UpdatePickupProgressUI(float progress)
    {
        if (isPlayer == false) return;
        if (pickupProgressBar != null)
            pickupProgressBar.fillAmount = progress;
    }

    private void ResetPickupProgressUI()
    {
        if (pickupProgressBar != null)
            pickupProgressBar.fillAmount = 0f;
    }
}
