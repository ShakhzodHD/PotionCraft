using System.Collections;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    private static PlayerSoundManager playerSoundManager;
    public static PlayerSoundManager manager
    {
        get { return playerSoundManager; }
        set { playerSoundManager = value; }
    }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] runningFloor;
    [SerializeField] private AudioClip[] runningTerrain;
    [SerializeField] private AudioClip potion;
    [SerializeField] private AudioClip spawnObj;
    [SerializeField] private AudioClip buyDecor;
    [SerializeField] private AudioClip upgradeSound;
    [SerializeField] private AudioClip canselSound;
    [SerializeField] private AudioClip putItemCraft;

    [SerializeField] private CharacterController characterController;

    private string currentSurface = "";
    private int currentSoundIndex;
    private bool isFootstepSoundPlaying = false;
    private void Awake()
    {
        if (manager != null && manager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void FixedUpdate()
    {
        bool isMoving = characterController.isGrounded && characterController.velocity.magnitude > 0.1f;

        if (isMoving)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1.0f))
            {
                string surfaceTag = hit.collider.tag;

                if (surfaceTag != currentSurface)
                {
                    currentSurface = surfaceTag;
                }
            }
        }
    }
    private void FootstepSound() //Called from Run animation
    {
        if (isFootstepSoundPlaying == true) return;
        isFootstepSoundPlaying = true;

        if (currentSurface == "Floor")
        {
            PlaystepSound(runningFloor);
        }
        else
        {
            PlaystepSound(runningTerrain);
        }
    }
    private void PlaystepSound(AudioClip[] clips)
    {
        currentSoundIndex = Mathf.FloorToInt(Random.Range(0, clips.Length));
        audioSource.PlayOneShot(clips[currentSoundIndex]);
        StartCoroutine(ResetFootstepFlag(clips[currentSoundIndex].length));
    }
    private IEnumerator ResetFootstepFlag(float delay)
    {
        yield return new WaitForSeconds(delay);
        isFootstepSoundPlaying = false;
    }
    public void PlayHarvestSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void PlayCraftPotionSound()
    {
        audioSource.PlayOneShot(potion);
    }
    public void PlaySpawnObjSound()
    {
        audioSource.PlayOneShot(spawnObj);
    }
    public void PlayBuyDecorSound() 
    {
        audioSource.PlayOneShot(buyDecor);
    }
    public void PlayUpgradeSound()

    {
        audioSource.PlayOneShot(upgradeSound);
    }
    public void PlayCanselSound()
    {
        audioSource.PlayOneShot(canselSound);
    }
    public void PlayPutItemCraft()
    {
        audioSource.PlayOneShot(putItemCraft);
    }
}
