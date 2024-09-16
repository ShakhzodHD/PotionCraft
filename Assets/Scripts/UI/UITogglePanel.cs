using UnityEngine;

public class UITogglePanel : MonoBehaviour
{
    public void TogglePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
