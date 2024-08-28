using UnityEngine;

public class ChangeColors : MonoBehaviour
{
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private Material baseMaterial;
    [SerializeField] private float duration = 2f;

    private void Start()
    {
        baseMaterial.color = startColor;
    }
    public void ChangeColor()
    {
        if (baseMaterial != null)
        {
            baseMaterial.color = startColor;
            StartCoroutine(ChangeColorOverTime());
        }
        else
        {
            Debug.LogError("Material not assigned!");
        }

    }

    private System.Collections.IEnumerator ChangeColorOverTime()
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            baseMaterial.color = Color.Lerp(startColor, endColor, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;  
        }
        baseMaterial.color = endColor;
    }
}
