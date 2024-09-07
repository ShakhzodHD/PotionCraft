using UnityEngine;

public class VisualozationLookAtCamera : MonoBehaviour
{
    private void Update()
    {
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}
