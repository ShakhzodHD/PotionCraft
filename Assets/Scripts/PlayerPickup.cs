using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerPickup : MonoBehaviour
{
    [HideInInspector] public int inventoryLimit;
    [HideInInspector] public float pickupDelay;

    [SerializeField] private Image pickupProgressBar; 

    private bool isPickingUp = false;
    private Coroutine pickupCoroutine;

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
            objToPickUp.transform.SetParent(transform);
            objToPickUp.tag = "UnPickupable";
            meshRenderer.enabled = true;
        }
        else if (objToPickUp.CompareTag("Selling"))
        {
            objToPickUp.transform.SetParent(transform);
        }

        ResetPickupProgressUI();
        isPickingUp = false;
    }

    private void UpdatePickupProgressUI(float progress)
    {
        if (pickupProgressBar != null)
            pickupProgressBar.fillAmount = progress;
    }

    private void ResetPickupProgressUI()
    {
        if (pickupProgressBar != null)
            pickupProgressBar.fillAmount = 0f;
    }
}
