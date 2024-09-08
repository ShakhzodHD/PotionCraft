using UnityEngine;
using YG;

public class YGRewardedManager : MonoBehaviour
{
    [SerializeField] private UpgradeSystem upgradeSystem;
    public void PlayRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }
    public void RewardUpgradeMovementSpeed(GameObject current)
    {
        upgradeSystem.UpgradeSpeedMovement(true);
        Destroy(current);
    }
    public void RewardUpgradeCapacity(GameObject current)
    {
        upgradeSystem.UpgradeCapacity(true);
        Destroy(current);
    }
    public void RewardUpgradeSpeedAction(GameObject current)
    {
        upgradeSystem.UpgradeSpeedAction(true);
        Destroy(current);
    }
}
