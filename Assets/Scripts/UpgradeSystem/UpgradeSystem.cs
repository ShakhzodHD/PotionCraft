using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    UpgradeManager upgradeManager = new UpgradeManager();

    [SerializeField] private SpeedMovementUpgrade speedMovementUpgrade;
    [SerializeField] private CapacityUpgrade capacityUpgrade;
    [SerializeField] private SpeedActionUpgrade speedActionUpgrade;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            upgradeManager.ApplyUpgrade(speedMovementUpgrade);
            upgradeManager.GetUpgradePrice(speedMovementUpgrade);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            upgradeManager.ApplyUpgrade(capacityUpgrade);
            upgradeManager.GetUpgradePrice(capacityUpgrade);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            upgradeManager.ApplyUpgrade(speedActionUpgrade);
            upgradeManager.GetUpgradePrice(speedActionUpgrade);
        }
    }
}
