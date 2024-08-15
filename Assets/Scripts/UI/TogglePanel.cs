using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    [SerializeField] private UIManager uiManager; 

    public void TogglePanelVisibility()
    {
        if (uiManager != null)
        {
            uiManager.ToggleAdditionalPanel(); 
        }
    }
}
