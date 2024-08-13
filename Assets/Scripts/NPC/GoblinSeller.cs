using System.Collections;
using UnityEngine;

public class GoblinSeller : MonoBehaviour
{
    [SerializeField] private ProcessExchange exchange;
    private void Start()
    {
        Transform child = exchange.transform.GetChild(0);
        Destroy(child.gameObject);
        StartCoroutine(PlayerReady());
    }
    private IEnumerator PlayerReady()
    {
        while (true)
        {
            exchange.inReadyPlayer = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
