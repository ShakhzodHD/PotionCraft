using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform panel1; 
    [SerializeField] private RectTransform panel2; 
    [SerializeField] private RectTransform additionalPanel; 

    [Header("Outline")]
    [SerializeField] private Outline outlinePlayer;

    private RectTransform activePanel = null; 

    public void SetPanelState(RectTransform panel, bool isActive)
    {
        if (activePanel != panel) 
        {
            if (activePanel != null)
            {
                activePanel.gameObject.SetActive(false);
            }

            activePanel = panel;
            panel.gameObject.SetActive(isActive);

            UpdateOutlineState(isActive);
        }
        else
        {
            panel.gameObject.SetActive(isActive);
            UpdateOutlineState(isActive);
        }
    }

    private void UpdateOutlineState(bool isPanelActive)
    {
        if (outlinePlayer != null)
        {
            if (isPanelActive)
            {
                outlinePlayer.DisableOutline();
            }
            else
            {
                outlinePlayer.EnableOutline();
            }
        }
    }

    public void ToggleAdditionalPanel()
    {
        if (additionalPanel != null)
        {
            bool isActive = !additionalPanel.gameObject.activeSelf;
            SetPanelState(additionalPanel, isActive);
        }
    }

    public RectTransform GetActivePanel()
    {
        return activePanel;
    }

    public void HideAllPanels()
    {
        if (activePanel != null)
        {
            activePanel.gameObject.SetActive(false);
            activePanel = null;
            UpdateOutlineState(false);
        }
    }

    public void HideAdditionalPanel()
    {
        if (additionalPanel != null && additionalPanel.gameObject.activeSelf)
        {
            additionalPanel.gameObject.SetActive(false);
            if (outlinePlayer != null)
            {
                outlinePlayer.EnableOutline();
            }
        }
    }
}
