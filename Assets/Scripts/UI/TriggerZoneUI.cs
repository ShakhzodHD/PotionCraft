using UnityEngine;

public class TriggerZoneUI : MonoBehaviour
{
    [SerializeField] private UIManager uiManager; 
    [SerializeField] private RectTransform panelToActivate; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.SetPanelState(panelToActivate, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiManager.GetActivePanel() == panelToActivate)
            {
                uiManager.HideAllPanels(); 
            }
        }
    }
}
