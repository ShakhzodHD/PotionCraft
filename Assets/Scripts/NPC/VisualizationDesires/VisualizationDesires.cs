using UnityEngine;
using System.Collections.Generic;
using static Resource;
using System;

public class VisualizationDesires : MonoBehaviour
{
    [SerializeField] private GameObject visualPrefab;
    [SerializeField] private Sprite[] logos;

    private Dictionary<Potions, int> desireToLogoIndex = new Dictionary<Potions, int>
    {
        { Potions.Regen, 0 },
        { Potions.Power, 1 },
        { Potions.Necromancy, 2 },
        { Potions.Curse, 3 }
    };

    public void AddVisual(string desire)
    {
        // ѕопытка преобразовани€ строки в значение enum Potions
        if (Enum.TryParse(desire, out Potions parsedDesire) 
            && desireToLogoIndex.TryGetValue(parsedDesire, out int logoIndex) 
            && logoIndex < logos.Length)
        {
            GameObject visualInstance = Instantiate(visualPrefab, transform);

            visualInstance.GetComponent<SpriteRenderer>().sprite = logos[logoIndex];
        }
        else
        {
            Debug.LogWarning($"No matching logo found for desire: {desire}");
        }
    }
}
