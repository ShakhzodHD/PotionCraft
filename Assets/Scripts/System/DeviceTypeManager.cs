using UnityEngine;
using YG;

public class DeviceTypeManager : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private Joystick currentJoystick;
    public void DefineDivaceType()
    {
        if (YandexGame.EnvironmentData.isMobile)
        {
            controller.isMovile = true;
            Instantiate(currentJoystick);
        }
        else
        {
            controller.isMovile = false;
            Destroy(currentJoystick.gameObject);
        }
    }
}
