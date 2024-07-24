using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDoor : MonoBehaviour
{
    [SerializeField] private float maxOpenTime = 2.0f;
    [SerializeField] private float speed = 1.0f;

    [SerializeField] private Transform doorLeft;
    [SerializeField] private Transform doorRight;

    private Quaternion doorLeftClosedRotation;
    private Quaternion doorRightClosedRotation;
    private Quaternion doorLeftOpenRotation;
    private Quaternion doorRightOpenRotation;

    private float currentOpenTime = 0.0f;
    private bool isPlayerNear = false;

    private void Start()
    {
        doorLeftClosedRotation = doorLeft.rotation;
        doorRightClosedRotation = doorRight.rotation;

        doorLeftOpenRotation = doorLeftClosedRotation * Quaternion.Euler(0, 90, 0);
        doorRightOpenRotation = doorRightClosedRotation * Quaternion.Euler(0, -90, 0);
    }
        
    private void Update()
    {
        if (isPlayerNear)
        {
            currentOpenTime = Mathf.Min(currentOpenTime + speed * Time.deltaTime, maxOpenTime);
        }
        else
        {
            currentOpenTime = Mathf.Max(currentOpenTime - speed * Time.deltaTime, 0);
        }

        float t = currentOpenTime / maxOpenTime;
        doorLeft.rotation = Quaternion.Slerp(doorLeftClosedRotation, doorLeftOpenRotation, t);
        doorRight.rotation = Quaternion.Slerp(doorRightClosedRotation, doorRightOpenRotation, t);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Buyer"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Buyer"))
        {
            isPlayerNear = false;
        }
    }
}
