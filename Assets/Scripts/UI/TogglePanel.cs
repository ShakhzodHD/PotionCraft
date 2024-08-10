using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void TogglePanelVisibility()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
