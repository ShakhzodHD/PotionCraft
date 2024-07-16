using UnityEngine;

public class BuyZoneSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] buyZones;
    private int countZone = 0;
    public int CountZone
    {
        get { return countZone; }
        set
        {
            if (value > buyZones.Length - 1)  { return; }
            countZone = value;
            UpdateActiveBuyZone();
        }
    }
    private void Start()
    {
        UpdateActiveBuyZone();
    }
    private void UpdateActiveBuyZone()
    {
        buyZones[countZone].SetActive(true);
    }
}
