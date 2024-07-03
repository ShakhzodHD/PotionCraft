using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private int inventoryLimit = 2;
    [SerializeField] private float pickupDelay = 1f;
    [SerializeField] private Image pickupProgressBar; // Ссылка на изображение прогресс-бара

    private bool isPickingUp = false;
    private Coroutine pickupCoroutine;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Pickupable") && !other.CompareTag("Selling")) return;

        int temp = gameObject.transform.childCount;
        if (temp > inventoryLimit + 1 || isPickingUp) return;

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
        }
        else if (objToPickUp.CompareTag("Selling"))
        {
            objToPickUp.transform.SetParent(transform);
        }

        Debug.Log("Подобран объект: " + objToPickUp.name);

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
