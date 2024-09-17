using UnityEngine;
using UnityEngine.UI;
public class UIEnableCustomElement : MonoBehaviour
{
    [SerializeField] private GameObject wings;
    [SerializeField] private Image wingsElement;
    public bool StateElement
    {
        get { return wings.activeSelf; }
        set
        {
            if (value)
            {
                ActivateElement(wingsElement);
            }
            else
            {
                DeactivateElement(wingsElement);
            }
        }
    }
    public void ToggleElement()
    {
        if (wingsElement != null)
        {
            if (wings.activeSelf)
            {
                DeactivateElement(wingsElement);
            }
            else
            {
                ActivateElement(wingsElement);
            }
        }
    }

    private void ActivateElement(Image element)
    {
        element.color = Color.green;
        wings.SetActive(true);
    }

    private void DeactivateElement(Image element)
    {
        element.color = Color.white;
        wings.SetActive(false);
    }
}
