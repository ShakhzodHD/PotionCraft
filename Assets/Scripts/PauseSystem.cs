using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    private static PauseSystem instance;
    public static PauseSystem Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
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
