using UnityEngine;

public class PlaySpawnEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem spawnPlantEffect;

    [SerializeField] private Color particleColorFirst;
    [SerializeField] private Color particleColorSecond;

    public void Play()
    {
        if (spawnPlantEffect != null)
        {
            ParticleSystem effectInstance = Instantiate(spawnPlantEffect, transform.position, Quaternion.identity);

            var mainModule = effectInstance.main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(particleColorFirst, particleColorSecond);

            Destroy(effectInstance.gameObject, effectInstance.main.duration);
        }
        else
        {
            Debug.LogWarning("Spawn Plant Effect is not assigned.");
        }
    }
}
