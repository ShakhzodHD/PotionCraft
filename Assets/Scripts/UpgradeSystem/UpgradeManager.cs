using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public class UpgradeManager
{
    private Dictionary<string, IUpgrade> upgrades;
    public UpgradeManager()
    {
        upgrades = new Dictionary<string, IUpgrade>();
    }
    public void AddUpgrade(string key, IUpgrade upgrade)
    {
        if (!upgrades.ContainsKey(key))
        {
            upgrades.Add(key, upgrade);
        }
    }
    public int GetUpgradePrice(IUpgrade upgrades)
    {
        return upgrades.GetPrice();
    }
    public void ApplyUpgrade(IUpgrade upgrade)
    {
        upgrade.ApplyUpgrade();
    }
    public int GetUpgradeLevel(string key)
    {
        if (upgrades.ContainsKey(key))
        {
            return upgrades[key].Level;
        }
        return 0;
    }
}
