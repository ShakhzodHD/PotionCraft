using System.Collections;
using UnityEngine;

public class UISwitchingPages : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform[] panels;
    private void Start()
    {
        panels[0].gameObject.SetActive(true);
    }
    public void Switching(int index)
    {
        StartCoroutine(Enable(index));
        StartCoroutine(Disable());
    }
    private IEnumerator Enable(int index)
    {
        yield return new WaitForSeconds(1.5f);
        panels[index].gameObject.SetActive(true);
    }
    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].gameObject.SetActive(false);
        }
    }
}
