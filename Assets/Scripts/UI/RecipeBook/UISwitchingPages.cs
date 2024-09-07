using System.Collections;
using UnityEngine;

public class UISwitchingPages : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform[] panels;

    private bool isSwitching = false; 

    [Header("Page Effect and Sound")]
    [SerializeField] private CurlPageEffect curlPageEffect;
    [SerializeField] private UISoundManager uiSoundManager;

    private void Start()
    {
        panels[0].gameObject.SetActive(true);
    }

    public void Switching(int index)
    {
        if (isSwitching) return;

        StartCoroutine(SwitchPanel(index));
    }

    private IEnumerator SwitchPanel(int index)
    {
        isSwitching = true; 

        curlPageEffect.StartAnimation();
        uiSoundManager.PlayFlipPage();

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);

        panels[index].gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        isSwitching = false;
    }
}
