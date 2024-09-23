using UnityEngine;
using YG;

public class DeviceTypeManager : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private Joystick currentJoystick;
    [SerializeField] private RectTransform throwButton;
    public void DefineDivaceType()
    {
        if (YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
        {
            controller.isMovile = true;
            Instantiate(currentJoystick);
            Instantiate(throwButton);
        }
        else
        {
            controller.isMovile = false;
            Destroy(currentJoystick.gameObject);
            Destroy(throwButton.gameObject);
        }
    }
}
