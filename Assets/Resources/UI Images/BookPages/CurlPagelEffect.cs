using UnityEngine;
using UnityEngine.UI;

public class CurlPageEffect : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float scaleDuration;

    [SerializeField] private float targetRotation = 180.0f;
    [SerializeField] private float currentRotation = 0.0f;

    private RectTransform rectTransform;
    private Image image;

    private float scaleTime;
    private int cyclesCompleted;
    private bool isScaling = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (isScaling)
        {
            // Handle rotation
            currentRotation = Mathf.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            Vector3 currentEulerAngles = rectTransform.localEulerAngles;
            rectTransform.localEulerAngles = new Vector3(currentEulerAngles.x, currentRotation, currentEulerAngles.z);

            // Handle scaling
            scaleTime += Time.deltaTime;
            float progress = Mathf.Clamp01(scaleTime / scaleDuration);
            Color color = image.color;
            color.a = Mathf.Lerp(0f, 1f, Mathf.PingPong(progress * 2, 1.0f));
            image.color = color;
            float scaleX = Mathf.Lerp(1.0f, 0.7f, Mathf.PingPong(progress * 2, 1.0f));
            rectTransform.localScale = new Vector3(scaleX, rectTransform.localScale.y, rectTransform.localScale.z);

            if (scaleTime >= scaleDuration)
            {
                scaleTime = 0.0f;
                cyclesCompleted++;
                if (cyclesCompleted >= 1)
                {
                    isScaling = false;
                    rectTransform.localScale = new Vector3(1.0f, rectTransform.localScale.y, rectTransform.localScale.z);
                }
            }
        }
    }

    public void StartAnimation()
    {
        // Reset the parameters
        currentRotation = 0.0f;
        scaleTime = 0.0f;
        cyclesCompleted = 0;

        // Set the flag to start scaling
        isScaling = true;
    }
}
