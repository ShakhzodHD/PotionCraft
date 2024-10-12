using UnityEngine;

public class ThrowingText : MonoBehaviour
{
    [SerializeField] private IndicatorCountItemsUI indicator;
    private void OnTriggerEnter(Collider other)
    {
        indicator.FadeInText();
    }
    private void OnTriggerExit(Collider other)
    {
        indicator.FadeOutText();
    }
}
