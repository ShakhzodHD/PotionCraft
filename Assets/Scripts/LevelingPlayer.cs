using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingPlayer : MonoBehaviour
{
    [Header("Increase Amount")]
    [SerializeField] private float speed;
    [SerializeField] private int capacity;
    [SerializeField] private float actionSpeed;

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
        playerController.moveSpeed = playerController.moveSpeed + speed;
    }
}
