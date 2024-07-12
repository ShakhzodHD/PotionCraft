using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProcessExchange : MonoBehaviour
{
    [SerializeField] private Image putProgressBar;
    [SerializeField] private float putDelay = 1f;

    public bool isTradeable = false;
    public bool inReadyPlayer = false;
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
            StartCoroutine(Trade());
        }
    }

    private IEnumerator Trade()
    {
        float timer = 0f;

        while (timer < putDelay)
        {
            timer += Time.deltaTime;
            UpdatePutProgressUI(timer / putDelay);
            yield return null;
        }

        CurrencyManager.instance.AddCurrency(20);
        isTradeable = true;
        inReadyPlayer = false;
        inReadyBuyer = false;
        UpdatePutProgressUI(0);
    }
    public void UpdatePutProgressUI(float progress)
    {
        if (putProgressBar != null)
        {
            putProgressBar.fillAmount = progress;
        }
    }
}
