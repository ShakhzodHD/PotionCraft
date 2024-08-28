using UnityEngine;

public class PlaySpawnSound : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    public void Play()
    {
        PlayerSoundManager.manager.PlayHarvestSound(clip);
    }
}
