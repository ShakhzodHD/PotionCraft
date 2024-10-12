using System.Collections;
using TMPro;
using UnityEngine;

public class IndicatorCountItemsUI : MonoBehaviour
{
    [SerializeField] private StoragePlants _storagePlants;
    [SerializeField] private StorageCrystal _crystal;

    [SerializeField] private TextMeshPro _textMeshPro;
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private int maxCount;
    [SerializeField] private bool isCrystal;
    private void OnEnable()
    {
        _storagePlants.OnLocalCountChanged += UpdateText;
        if (isCrystal)
        {
            _crystal.OnLocalCrystalCountChanged += UpdateTextCrystal;
        }
    }

    private void OnDisable()
    {
        _storagePlants.OnLocalCountChanged -= UpdateText;
        if (isCrystal)
        {
            _crystal.OnLocalCrystalCountChanged -= UpdateTextCrystal;
        }
    }
    private void UpdateText(int newCount)
    {
        PlayerSoundManager.manager.PlayPutItemCraft();
        
        if (isCrystal)
        {
            _textMeshPro.text = $"{newCount.ToString()} / {maxCount}\n{_crystal.LocalCount} / 1";
        }
        else
        {
            _textMeshPro.text = $"{newCount.ToString()} / {maxCount}";
        }
    }
    private void UpdateTextCrystal(int newCount)
    {
        PlayerSoundManager.manager.PlayPutItemCraft();
        _textMeshPro.text = $"{_storagePlants.LocalCount} / {maxCount}\n{newCount.ToString()} / 1";
    }
    private void Start()
    {
        if (isCrystal)
        {
            _textMeshPro.text = $"{_storagePlants.LocalCount} / {maxCount}\n{_crystal.LocalCount} / 1";

        }
        else
        {
            _textMeshPro.text = $"{_storagePlants.LocalCount} / {maxCount}";
        }
    }
    public void FadeOutText()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    public void FadeInText()
    {
        StartCoroutine(FadeInCoroutine());
    }
    private IEnumerator FadeOutCoroutine()
    {
        Color originalColor = _textMeshPro.color;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            _textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        _textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }

    private IEnumerator FadeInCoroutine()
    {
        Color originalColor = _textMeshPro.color;
        _textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            _textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        _textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
    }
}
