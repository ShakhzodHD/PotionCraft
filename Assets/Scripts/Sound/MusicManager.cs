using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] musicClips;
    private AudioSource audioSource;
    private int currentTrackIndex = 0;
    private float trackTime = 0f;

    private void Awake()
    {
        if (FindObjectsOfType<MusicManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        PlayNextTrack();
    }

    private void Update()
    {
        if (!audioSource.isPlaying && !IsPaused())
        {
            PlayNextTrack();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        if (musicClips.Length > 0)
        {
            audioSource.clip = musicClips[currentTrackIndex];
            audioSource.time = trackTime; 
            audioSource.Play();
            currentTrackIndex = (currentTrackIndex + 1) % musicClips.Length;
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            trackTime = audioSource.time;
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus)
        {
            trackTime = audioSource.time;
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }

    private bool IsPaused()
    {
        return !audioSource.isPlaying && audioSource.time > 0f;
    }
}
