using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class UpgradeManager : MonoBehaviour
{
    public Dictionary<Type, AbstractUpgrade> UpgradeDict;
    public static UpgradeManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        UpgradeDict = GetComponentsInChildren<AbstractUpgrade>().ToDictionary(type => type.GetType());
    }
}
