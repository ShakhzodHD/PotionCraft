using TMPro;
using UnityEngine;

public class ShopZone : MonoBehaviour
{
    [SerializeField] private BuyZoneSystem buyZoneSystem;
    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private AudioSource audioSource;

    [Header("Parameters about the object being purchased")]
    [SerializeField] GameObject itemToBuy;
    [SerializeField] private int fullPrice;
    [SerializeField] private int itemCost;

    [SerializeField] private float framesPerUpdate = 3;

    private float frameCount = 0;
    private void Start()
    {
        priceText.text = fullPrice.ToString();
    }
    private void OnTriggerStay(Collider other)
    {
        frameCount++;
        if (frameCount >= framesPerUpdate)
        {
            if (other.CompareTag("Player"))
            {
                if (CurrencyManager.instance.CanAfford(itemCost))
                {
                    CurrencyManager.instance.SpendCurrency(itemCost);
                    fullPrice--;
                    priceText.text = fullPrice.ToString();
                    if (!audioSource.isPlaying)
                    {
                        audioSource.Play();
                    }
                }
                else
                {
                    Debug.Log("Not enough currency to buy the item!");
                    audioSource.Pause();
                }
            }
            frameCount = 0;
        }
    }
    private void Update()
    {
        BuyItem();
    }
    private void BuyItem()
    {
        if (fullPrice <= 0)
        {
            buyZoneSystem.CountZone++;
            itemToBuy.SetActive(true);

            PlayerSoundManager.manager.PlaySpawnObjSound();

            Destroy(gameObject);
        }
    }
}
