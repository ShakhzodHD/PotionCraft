using UnityEngine;

public class ProcessExchange : MonoBehaviour
{
    public bool isTradeable = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isTradeable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isTradeable = false;
        }
    }
}
