using UnityEngine;
using UnityEngine.UI;
using YG;

public class ResetProgress : MonoBehaviour
{
    [SerializeField] private Button reset;
    public void SetResetProgress()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
}
