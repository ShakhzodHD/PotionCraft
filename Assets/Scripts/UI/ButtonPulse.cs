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
        buttonImage = GetComponent<Image>();  // �������� ��������� Image ������
        lerpTime = 0f;  // ���������� ������ � ������ ���������
    }

    private void Update()
    {
        lerpTime += Time.deltaTime * pulseSpeed;  // ����������� ����� ���������
        float lerpValue = Mathf.PingPong(lerpTime, 1);  // �������� �������� ��� ������������ �� 0 �� 1

        // ������������� ����� startColor � endColor �� ������ lerpValue
        buttonImage.color = Color.Lerp(startColor, endColor, lerpValue);
    }
}
