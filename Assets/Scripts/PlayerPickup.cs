using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private ResourceGenerator generator;
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
            generator.OnTakenObj();
        }
        if (objToPickUp.CompareTag("Selling"))
        {
            objToPickUp.transform.SetParent(transform);
        }

    }
}
