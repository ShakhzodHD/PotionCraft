using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    protected int _level;
    protected int _basePrice;
    public int Level => _level;
    public Upgrade(int basePrice)
    {
        _level = 0;
        _basePrice = basePrice;
    }
    public abstract int GetPrice();
    public virtual void ApplyUpgrade()
    {
        _level++;
    }
}
