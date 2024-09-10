using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchingLevels : MonoBehaviour
{
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
