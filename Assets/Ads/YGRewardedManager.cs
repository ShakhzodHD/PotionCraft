using UnityEngine;
using YG;

public class YGRewardedManager : MonoBehaviour
{
    [SerializeField] private UpgradeSystem upgradeSystem;
    [SerializeField] private UIButtonManager buttonManager;

    private int pendingButtonIndex = -1;

    private const int ButtonEffectAdId = 100;
    private const int MovementSpeedAdId = 200;
    private const int CapacityAdId = 201;
    private const int ActionSpeedAdId = 202;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += OnRewardedVideoAdCompleted;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= OnRewardedVideoAdCompleted;
    }

    public void PlayRewardAdForButtonEffect(int buttonIndex)
    {
        pendingButtonIndex = buttonIndex;
        YandexGame.RewVideoShow(ButtonEffectAdId + buttonIndex); 
    }

    public void PlayRewardAdForMovementSpeed(GameObject current)
    {
        YandexGame.RewVideoShow(MovementSpeedAdId);
        Destroy(current);
    }

    public void PlayRewardAdForCapacity(GameObject current)
    {
        YandexGame.RewVideoShow(CapacityAdId);
        Destroy(current);
    }

    public void PlayRewardAdForActionSpeed(GameObject current)
    {
        YandexGame.RewVideoShow(ActionSpeedAdId);
        Destroy(current);
    }

    // This method occurs when the ad ends.
    private void OnRewardedVideoAdCompleted(int id)
    {
        if (id >= ButtonEffectAdId && id < ButtonEffectAdId + buttonManager.GetButtonCount())
        {
            int buttonIndex = id - ButtonEffectAdId;
            buttonManager.UnlockEffect(buttonIndex);
        }
        else if (id == MovementSpeedAdId)
        {
            upgradeSystem.UpgradeSpeedMovement(true);
        }
        else if (id == CapacityAdId)
        {
            upgradeSystem.UpgradeCapacity(true);
        }
        else if (id == ActionSpeedAdId)
        {
            upgradeSystem.UpgradeSpeedAction(true);
        }
    }
}
