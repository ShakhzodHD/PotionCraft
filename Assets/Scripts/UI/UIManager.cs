using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform panel1; 
    [SerializeField] private RectTransform panel2;
    [SerializeField] private RectTransform escPanel;
    [SerializeField] private RectTransform guidPanel;
    [SerializeField] private RectTransform tutorialPanel;

    [SerializeField] private RectTransform additionalPanel;
    [SerializeField] private RectTransform controlsPanel;

    [Header("Outline")]
    [SerializeField] private Outline outlinePlayer;

    [Header("Pause")]
    [SerializeField] private PauseSystem pause;

    private RectTransform activePanel = null;
    private bool isAnyWindowOpen = false;
    public bool IsAnyWindowOpen
    {
        get { return isAnyWindowOpen; }
        set { isAnyWindowOpen = value; }
    }

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
            IsAnyWindowOpen = isActive;

            UpdateOutlineState(isActive);
        }
        else
        {
            panel.gameObject.SetActive(isActive);
            UpdateOutlineState(isActive);
            IsAnyWindowOpen = isActive;
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
            pause.RemovePause();
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
            IsAnyWindowOpen = false;
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
    public void ToggleEscPanel()
    {
        if (escPanel != null)
        {
            bool isActive = !escPanel.gameObject.activeSelf;
            SetPanelState(escPanel, isActive);
            
            if (isActive == true)
            {
                pause.SetPause();
            }
            else
            {
                pause.RemovePause();
            }
        }
    }
    public void ToggleGuidPanel()
    {
        if (guidPanel != null)
        {
            bool isActive = !guidPanel.gameObject.activeSelf;
            SetPanelState(guidPanel, isActive);
        }
    }
    public void ToggleTutorialPanel()
    {
        if (tutorialPanel != null)
        {
            bool isActive = !tutorialPanel.gameObject.activeSelf;
            SetPanelState(tutorialPanel, isActive);

            if (isActive == true)
            {
                pause.SetPause();
            }
            else
            {
                pause.RemovePause();
            }
        }
    }
    public void ToggleControlslPanel()
    {
        if (controlsPanel != null)
        {
            bool isActive = !controlsPanel.gameObject.activeSelf;
            SetPanelState(controlsPanel, isActive);

            if (isActive == true)
            {
                pause.SetPause();
            }
            else
            {
                pause.RemovePause();
            }
        }
    }
}
