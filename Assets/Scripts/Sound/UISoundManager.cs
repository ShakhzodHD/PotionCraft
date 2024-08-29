using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip flipPageSound;
    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
    }
    public void PlayFlipPage()
    {
        audioSource.PlayOneShot(flipPageSound);
    }
}
