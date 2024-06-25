using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
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
