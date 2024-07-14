using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgrade 
{
    int Level { get; }
    int GetPrice();
    void ApplyUpgrade();
}
