using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryngAnimation : MonoBehaviour
{
    [SerializeField] int countItem;

    private int number;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        number = transform.childCount;
        if (number > countItem)
        {
            anim.SetLayerWeight(1, 1);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
    }
}
