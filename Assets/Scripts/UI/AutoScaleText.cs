using UnityEngine;
using UnityEngine.UI;

public class AutoScaleText : MonoBehaviour
{
    [SerializeField] private Text[] uiTextS;
    [SerializeField] private float scaleFactor;

    private int currentScreenWidth;
    private int currentScreenHeight;

    void Start()
    {
        currentScreenWidth = Screen.width;
        currentScreenHeight = Screen.height;

        UpdateTextSize();
    }

    void Update()
    {
        if (Screen.width != currentScreenWidth || Screen.height != currentScreenHeight)
        {
            currentScreenWidth = Screen.width;
            currentScreenHeight = Screen.height;

            UpdateTextSize();
        }
    }

    private void UpdateTextSize()
    {
        float canvasWidth = Screen.width;
        float canvasHeight = Screen.height;

        foreach (var text in uiTextS)
        {
            text.fontSize = Mathf.RoundToInt(Mathf.Min(canvasWidth, canvasHeight) * scaleFactor);
        }
    }
}
