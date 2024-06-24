using UnityEngine;

public class ProcessExchange : MonoBehaviour
{
    public bool isTradeable = false;
    private bool inReadyPlayer = false;
    private bool inReadyBuyer = false;
    private void OnTriggerEnter(Collider other)
    {
        if (inReadyPlayer == true && inReadyBuyer == true)
        {
            Trade();
        }
        if (other.tag == "Player")
        {
            inReadyPlayer = true;
        }
        if (other.tag == "Buyer")
        {
            inReadyBuyer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isTradeable = false;
            inReadyPlayer = false;
        }
        if (other.tag == "Buyer")
        {
            inReadyBuyer = false;
        }
    }
    private void Trade()
    {
        CurrencyManager.instance.AddCurrency(20);
        isTradeable = true;
        inReadyPlayer = false;
    }
}
