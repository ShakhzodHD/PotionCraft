using UnityEngine;

public class ProcessExchange : MonoBehaviour
{
    public bool isTradeable = false;
    private bool inReadyPlayer = false;
    private bool inReadyBuyer = false;

    public void SetPlayerReady(bool ready)
    {
        inReadyPlayer = ready;
        CheckTrade();
    }

    public void SetBuyerReady(bool ready)
    {
        inReadyBuyer = ready;
        CheckTrade();
    }

    private void CheckTrade()
    {
        if (inReadyPlayer && inReadyBuyer)
        {
            Trade();
        }
    }

    private void Trade()
    {
        CurrencyManager.instance.AddCurrency(20);
        isTradeable = true;
        inReadyPlayer = false;
        inReadyBuyer = false;
    }
}
