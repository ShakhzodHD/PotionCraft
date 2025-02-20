using UnityEngine;
using UnityEngine.UI;

public class DisableInteracbleUI : MonoBehaviour
{
    private static DisableInteracbleUI instance;
    public static DisableInteracbleUI Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Button[] buttons;
    public void Disable()
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
    }
    public void Enable()
    {
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
    }
}
