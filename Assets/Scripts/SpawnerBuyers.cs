using UnityEngine;

public class SpawnerBuyers : MonoBehaviour
{
    [SerializeField] private GameObject buyerPrefab;
    [SerializeField] private float spawnInterval = 3f;

    private void Start()
    {
        InvokeRepeating("Spawner", 0.0f, spawnInterval);
    }
    private void Spawner()
    {
        Instantiate(buyerPrefab, gameObject.transform.position, gameObject.transform.rotation);
    }
}
