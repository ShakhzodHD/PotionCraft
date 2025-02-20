using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class YGInterlineAds : MonoBehaviour
{
    private float coolDown = 90;
    private float timerShowAd = 0;
    [SerializeField] private UIManager manager;
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

    private void Update()
    {
        timerShowAd += Time.deltaTime;
    }

    private IEnumerator InvokeShowFullScreenAd()
    {
        while (true)
        {
            Debug.Log($"Timer: {timerShowAd}, isAdShowing: {isAdShowing}, AnyWindowOpen: {manager.IsAnyWindowOpen}");

            if (timerShowAd >= coolDown && !isAdShowing && !manager.IsAnyWindowOpen)
            {
                yield return new WaitForSeconds(1);
                if (manager.IsAnyWindowOpen) continue;

                adsPanel.gameObject.SetActive(true);
                baseText = timerText.text;

                while (currentCountdownTime > 0)
                {
                    timerText.text = baseText + Mathf.Ceil(currentCountdownTime);
                    PauseSystem.Instance.SetPause();
                    DisableInteracbleUI.Instance.Disable();
                    yield return null;
                    currentCountdownTime -= Time.unscaledDeltaTime;
                }

                ShowFullScreenAd();
                adsPanel.gameObject.SetActive(false);
                currentCountdownTime = countdownTime;
                timerShowAd = 0;
            }
            else
            {
                adsPanel.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    public void ShowFullScreenAd()
    {
        isAdShowing = true;
        YandexGame.FullscreenShow();
        isAdShowing = false;
    }
}
