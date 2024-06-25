using UnityEngine;

public class TriggerZoneForExchange : MonoBehaviour
{
    public bool isPlayerZone;
    private ProcessExchange processExchange;

    private void Start()
    {
        processExchange = GetComponentInParent<ProcessExchange>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayerZone && other.CompareTag("Player"))
        {
            processExchange.SetPlayerReady(true);
        }
        else if (!isPlayerZone && other.CompareTag("Buyer"))
        {
            if (other.transform.childCount > 0)
            {
                processExchange.SetBuyerReady(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlayerZone && other.CompareTag("Player"))
        {
            processExchange.SetPlayerReady(false);
        }
        else if (!isPlayerZone && other.CompareTag("Buyer"))
        {
            processExchange.SetBuyerReady(false);
        }
    }

}
