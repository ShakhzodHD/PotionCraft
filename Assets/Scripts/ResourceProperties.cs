using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProperties : MonoBehaviour
{
    /*[HideInInspector]*/ public string typeResource;
    [SerializeField] private CraftingSystem.itemCraft itemCraft;
    private void Start()
    {
        typeResource = itemCraft.ToString();
    }
}
