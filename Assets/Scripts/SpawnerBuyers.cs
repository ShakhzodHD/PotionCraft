using UnityEngine;

public class SpawnerBuyers : MonoBehaviour
{
    [SerializeField] private GameObject[] buyerPrefabs;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int maxBuyers;

    private int currentBuyerCount = 0;
    public int CurrentBuyerCount
    {
        get { return currentBuyerCount; }
        set
        {
            if (currentBuyerCount <= 0) return;

            currentBuyerCount = value;
        }
    }

    private void Start()
    {
        if (buyerPrefabs.Length > 0)
        {
            InvokeRepeating("Spawner", 0.0f, spawnInterval);
        }
    }

    private void Spawner()
    {
        if (currentBuyerCount >= maxBuyers) return;

        int index = Random.Range(0, buyerPrefabs.Length);
        GameObject customerPrefab = buyerPrefabs[index];

        Instantiate(customerPrefab, transform.position, Quaternion.identity);
        currentBuyerCount++;
    }
}