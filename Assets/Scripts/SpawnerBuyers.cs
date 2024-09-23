using UnityEngine;

public class SpawnerBuyers : MonoBehaviour
{
    [SerializeField] private GameObject[] buyerPrefabs;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int maxBuyers;

    [SerializeField] private float spawnIntervalReductionRate; // —колько времени уменьшать интервал спавна
    [SerializeField] private int maxBuyersIncreaseRate = 1; //  оличество увеличени€ покупателей
    [SerializeField] private float timeToIncrease; // ¬рем€ дл€ увеличени€ параметров (в секундах)

    private int currentBuyerCount = 0;
    private float timerToIncrease;

    private int currentIncreaseLevel = 0;

    public int CurrentBuyerCount
    {
        get { return currentBuyerCount; }
        set
        {
            if (currentBuyerCount <= 0) return;

            currentBuyerCount = value;
        }
    }
    public int CurrentIncreaseLevel
    {
        get { return currentIncreaseLevel; }
    }
    private void Start()
    {
        if (buyerPrefabs.Length > 0)
        {
            InvokeRepeating("Spawner", 0.0f, spawnInterval);
        }

        timerToIncrease = timeToIncrease;
    }

    private void Update()
    {
        timerToIncrease -= Time.deltaTime;

        if (timerToIncrease <= 0f)
        {
            IncreaseSpawnParameters();
            timerToIncrease = timeToIncrease;
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

    private void IncreaseSpawnParameters()
    {
        currentIncreaseLevel++;
        IncreaseValue();
    }
    public void IncreaseValue()
    {
        maxBuyers += maxBuyersIncreaseRate;

        spawnInterval = Mathf.Max(0.5f, spawnInterval - spawnIntervalReductionRate);

        CancelInvoke("Spawner");
        InvokeRepeating("Spawner", 0.0f, spawnInterval);
    }
}
