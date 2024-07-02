using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneUI : MonoBehaviour
{
    [SerializeField] private RectTransform objUI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objUI.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objUI.gameObject.SetActive(false);
        }
    }
}
