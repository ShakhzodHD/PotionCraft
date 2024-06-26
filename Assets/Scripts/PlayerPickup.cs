using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private int inventoryLimit = 2;
    private void OnTriggerEnter(Collider other)
    {
        int temp = gameObject.transform.childCount;
        if (temp > inventoryLimit + 1) return;
         
        PickUpObj(other.gameObject);
    }

    private void PickUpObj(GameObject objToPickUp)
    {
        if (objToPickUp.CompareTag("Pickupable"))
        {
            objToPickUp.transform.SetParent(transform);
            objToPickUp.tag = "UnPickupable";
        }
        if (objToPickUp.CompareTag("Selling"))
        {
            objToPickUp.transform.SetParent(transform);
        }
    }
}
