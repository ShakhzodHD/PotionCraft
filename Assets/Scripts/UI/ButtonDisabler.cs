using UnityEngine;
using UnityEngine.UI;
using YG;
public class ButtonDisabler : MonoBehaviour
{
    [Header("Upgrades")]
    [SerializeField] private SpeedMovementUpgrade speedMovementUpgrade;
    [SerializeField] private CapacityUpgrade capacityUpgrade;
    [SerializeField] private SpeedActionUpgrade speedActionUpgrade;

    [Header("Button")]
    [SerializeField] private Button buttonSpeedMovemen;
    [SerializeField] private Button buttonCapacity;
    [SerializeField] private Button buttonSpeedAction;
    public void TryButtonDisable()
    {
        TryDisable(speedMovementUpgrade, buttonSpeedMovemen);
        TryDisable(capacityUpgrade, buttonCapacity);
        TryDisable(speedActionUpgrade, buttonSpeedAction);
    }
    private void TryDisable<T>(T upgrade, Button button) where T : IUpgrade
    {
        if (upgrade is SpeedMovementUpgrade speedMovement)
        {
            int currentLevel = speedMovement.Level;
            Debug.Log("Текущее " + currentLevel + "макс " + speedMovement.MaxLevel);
            if (currentLevel == speedMovement.MaxLevel) 
            {
                button.gameObject.SetActive(false);
            }
        }
        if (upgrade is CapacityUpgrade capacity)
        {
            int currentLevel = capacity.Level;
            if (currentLevel == capacity.MaxLevel) 
            {
                button.gameObject.SetActive(false);
            }
        }
        if (upgrade is SpeedActionUpgrade speedAction)
        {
            int currentLevel = speedAction.Level;
            if (currentLevel == speedAction.MaxLevel)
            {
                button.gameObject.SetActive(false);
            }
        }
    }
}
