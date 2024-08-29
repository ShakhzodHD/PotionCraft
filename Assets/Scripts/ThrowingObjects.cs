using UnityEngine;

public class ThrowingObjects : MonoBehaviour
{
    [SerializeField] private PickupObject pickupObject;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("UnPickupable") || child.CompareTag("Selling"))
                {
                    pickupObject.RemoveItem(child.gameObject);
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
