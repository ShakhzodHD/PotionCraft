using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingPlayer : MonoBehaviour
{
    [Header("Increase Amount")]
    [SerializeField] private float speed;
    [SerializeField] private int capacity;
    [SerializeField] private float actionSpeed;

    [Header("Workers Goblins")]
    [SerializeField] private GameObject seller;
    [SerializeField] private GameObject regen;
    [SerializeField] private GameObject power;
    [SerializeField] private GameObject necromancy;
    [SerializeField] private GameObject curse;

    private PlayerController playerController;
    private PlayerPickup playerPickup;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerPickup = GetComponent<PlayerPickup>();

    }
    public void UpSpeed()
    {
        playerController.moveSpeed = playerController.moveSpeed + speed;
    }
    public void UpCapacity()
    {
        playerPickup.inventoryLimit = playerPickup.inventoryLimit + capacity;
    }
    public void UpActionSpeed()
    {
        playerPickup.pickupDelay = playerPickup.pickupDelay - actionSpeed;
        StorageManager.putDelay = StorageManager.putDelay - actionSpeed;
    }


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
