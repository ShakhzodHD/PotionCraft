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
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].gameObject.SetActive(false);
        }
        panels[index].gameObject.SetActive(true);
    }
}
