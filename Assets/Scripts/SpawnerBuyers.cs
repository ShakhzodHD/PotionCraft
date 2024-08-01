using UnityEngine;

public class SpawnerBuyers : MonoBehaviour
{
    [SerializeField] private GameObject[] buyerPrefab;
    [SerializeField] private float spawnInterval = 3f;

    private void Start()
    {
        InvokeRepeating("Spawner", 0.0f, spawnInterval);
    }
    private void Spawner()
    {
        int index = Random.Range(0, buyerPrefab.Length);
        GameObject customerPrefab = buyerPrefab[index];

        Instantiate(customerPrefab, transform.position, Quaternion.identity);
    }
}
