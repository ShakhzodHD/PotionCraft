using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Объект, за которым следит камера
    public Vector3 offset = new Vector3(0f, 10f, -10f); // Смещение камеры относительно цели
    public float smoothSpeed = 0.5f; // Скорость мягкого следования камеры за целью

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target.position); // Камера всегда направлена на цель
        }
    }
}
