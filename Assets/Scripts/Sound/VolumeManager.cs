using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider volumeMusic;
    [SerializeField] private Slider volumeSound;
    private void Start()
    {
        volumeMusic.onValueChanged.AddListener(SetMusicVolume);
        volumeSound.onValueChanged.AddListener(SetSoundVolume);
    }
    public void SetMusicVolume(float value)
    {
        // Конвертация значения слайдера (0 - 1) в децибелы (-80 - 0)
        float volume = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", value); // Сохраняем значение громкости
    }
    public void SetSoundVolume(float value)
    {
        float volume = Mathf.Log10(value) * 20;
        audioMixer.SetFloat("SoundVolume", volume);
        PlayerPrefs.SetFloat("SoundVolume", value); // Сохраняем значение громкости
    }
}
