using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class YGInterlineAds : MonoBehaviour
{
    [SerializeField] private int cooldowns;

    [SerializeField] private RectTransform adsPanel;
    [SerializeField] private Text timerText;

    private float countdownTime = 2f;
    private float currentCountdownTime; 
    private bool isAdShowing = false;

    private string baseText;

    private void Start()
    {
        if (cooldowns <= 60) { cooldowns = 60; }

        currentCountdownTime = countdownTime; 
        StartCoroutine(InvokeShowFullScreenAd());
    }

    private IEnumerator InvokeShowFullScreenAd()
    {
        while (true)
        {
            if (YandexGame.timerShowAd >= cooldowns && !isAdShowing)
            {
                adsPanel.gameObject.SetActive(true);

                baseText = timerText.text;

                while (currentCountdownTime > 0)
                {
                    timerText.text = baseText + Mathf.Ceil(currentCountdownTime);
                    PauseSystem.Instance.SetPause();
                    yield return null; 
                    currentCountdownTime -= Time.unscaledDeltaTime; 
                }

                ShowFullScreenAd();
                currentCountdownTime = countdownTime;
            }
            else
            {
                yield return null;
            }
        }
    }

    public void ShowFullScreenAd()
    {
        isAdShowing = true;
        YandexGame.FullscreenShow();
        adsPanel.gameObject.SetActive(false);

        isAdShowing = false;
    }
}
