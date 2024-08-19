using UnityEngine;

public class PotionPrice : MonoBehaviour
{
    [SerializeField] private int price;
    public int GetPrice()
    {
        return price;
    }
}
