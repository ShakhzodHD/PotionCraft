using UnityEngine;

public class ActivatorChangeColor : MonoBehaviour
{
    [SerializeField] private ChangeColors color;
    private void Start()
    {
        color.ChangeColor();
    }
}
