using UnityEngine;
using UnityEngine.UI;

public class ButtonPulseColor : MonoBehaviour
{
    [SerializeField] private Color startColor = Color.white;
    [SerializeField] private Color endColor = Color.red;
    [SerializeField] private float pulseSpeed = 1.0f; 

    private Image buttonImage;
    private float lerpTime;

    private void Start()
    {
        buttonImage = GetComponent<Image>();  // Получаем компонент Image кнопки
        lerpTime = 0f;  // Изначально начнем с начала интервала
    }

    private void Update()
    {
        lerpTime += Time.deltaTime * pulseSpeed;  // Увеличиваем время пульсации
        float lerpValue = Mathf.PingPong(lerpTime, 1);  // Получаем значение для интерполяции от 0 до 1

        // Интерполируем между startColor и endColor на основе lerpValue
        buttonImage.color = Color.Lerp(startColor, endColor, lerpValue);
    }
}
