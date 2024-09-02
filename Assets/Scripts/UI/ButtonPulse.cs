using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPulseColor : MonoBehaviour
{
    [SerializeField] private Color startColor = Color.white;
    [SerializeField] private Color endColor = Color.red;
    [SerializeField] private float pulseDuration = 0.45f; 

    private Image buttonImage;

    private float colorFactor; 
    private void Start()
    {
        buttonImage = GetComponent<Image>();
        StartCoroutine(PulseColor());
    }
    private IEnumerator PulseColor()
    {
        while (true) 
        {
            for (float t = 0; t < pulseDuration; t += Time.unscaledDeltaTime)
            {
                float normalizedTime = t / pulseDuration; 
                buttonImage.color = Color.Lerp(startColor, endColor, normalizedTime);
                yield return null; 
            }

            for (float t = 0; t < pulseDuration; t += Time.unscaledDeltaTime)
            {
                float normalizedTime = t / pulseDuration;
                buttonImage.color = Color.Lerp(endColor, startColor, normalizedTime);
                yield return null;
            }
        }
    }
    private void OnDestroy()
    {
        buttonImage.color = startColor;

    }
}
