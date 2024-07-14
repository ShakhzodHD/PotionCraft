using UnityEngine;

public class LevelingPlayer : MonoBehaviour
{
    [Header("Workers Goblins")]
    [SerializeField] private GameObject seller;
    [SerializeField] private GameObject regen;
    [SerializeField] private GameObject power;
    [SerializeField] private GameObject necromancy;
    [SerializeField] private GameObject curse;

    public void BuyGoblinSeller()
    {
        seller.SetActive(true);
    }
    public void BuyGoblinWorkerRegen()
    {
        regen.SetActive(true);
    }
    public void BuyGoblinWorkerPower()
    {
        power.SetActive(true);
    }
    public void BuyGoblinWorkerNecromancy()
    {
        necromancy.SetActive(true);
    }
    public void BuyGoblinWorkerCurse()
    {
        curse.SetActive(true);
    }
}
