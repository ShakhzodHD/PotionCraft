using UnityEngine;
using YG;

public class DeviceTypeManager : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(YandexGame.EnvironmentData.deviceType);
    }
}
