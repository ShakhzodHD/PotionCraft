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
            PotionPrice potionPrice = other.GetComponentInChildren<PotionPrice>();
            processExchange.SetBuyerReady(true);
            processExchange.SetPotionToSell(potionPrice);
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
