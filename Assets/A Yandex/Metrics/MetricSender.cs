using System.Collections.Generic;
using UnityEngine;
using YG;

public class MetricSender : MonoBehaviour
{
    [SerializeField] private string[] goals;
    public void Send(string id)
    {
        YandexMetrica.Send(id);
    }
    public void SendMetricOnce(int index)
    {
        if (YandexGame.savesData._isFirstClickButtons[index] == true)
        {
            YandexMetrica.Send(goals[index]);
            YandexGame.savesData._isFirstClickButtons[index] = false;
        }
    }
}
