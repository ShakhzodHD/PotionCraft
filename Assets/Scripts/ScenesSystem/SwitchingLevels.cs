using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchingLevels : MonoBehaviour
{
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
