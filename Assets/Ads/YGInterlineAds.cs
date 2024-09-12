using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class YGInterlineAds : MonoBehaviour
{
    [SerializeField] private RectTransform adsPanel;
    [SerializeField] private Text timerText;

    private float countdownTime = 2f;
    private float currentCountdownTime; 
    private bool isAdShowing = false;

    private string baseText;

    private void Start()
    {
        currentCountdownTime = countdownTime; 
        StartCoroutine(InvokeShowFullScreenAd());
    }

    private IEnumerator InvokeShowFullScreenAd()
    {
        while (true)
        {
            if (YandexGame.timerShowAd >= 60 && !isAdShowing)
            {
                adsPanel.gameObject.SetActive(true);
                PauseSystem.Instance.SetPause();

                baseText = timerText.text;

                while (currentCountdownTime > 0)
                {
                    timerText.text = baseText + Mathf.Ceil(currentCountdownTime);
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
        PauseSystem.Instance.RemovePause();

        isAdShowing = false;
    }
}
