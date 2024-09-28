using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class ResetProgress : MonoBehaviour
{
    [SerializeField] private Button reset;

    public void SetResetProgress()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
