using UnityEngine;

public class PassiveSourceGold : MonoBehaviour
{
    [SerializeField] private int goldPerInterval = 5;
    [SerializeField] private float intervalDuration = 5f;

    [SerializeField] private ParticleSystem spawnGoldEffect;
    [SerializeField] private Vector3 offsetPositionSpawn;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= intervalDuration)
        {
            AddGold();
            timer = 0f;
        }
    }

    private void AddGold()
    {
        if (CurrencyManager.instance != null)
        {
            CurrencyManager.instance.AddCurrency(goldPerInterval);

            Vector3 positionSpawn = transform.position + offsetPositionSpawn;
            ParticleSystem effectInstance = Instantiate(spawnGoldEffect, positionSpawn, Quaternion.Euler(-90, 0, 0));
            Destroy(effectInstance.gameObject, effectInstance.main.duration);
        }
    }
}
