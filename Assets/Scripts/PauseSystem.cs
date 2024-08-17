using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    private void Start()
    {
        RemovePause();
    }
    public void SetPause()
    {
        Time.timeScale = 0.0f;
    }
    public void RemovePause()
    {
        Time.timeScale = 1.0f;
    }
}
