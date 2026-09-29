using System;
using UnityEngine;

[Serializable]
public class EffectData
{
    public EffectType Type;
    public GameObject Prefab;
    [Min(0)] public int PoolSize = 5;
    [Min(0f)] public float Duration = 1f;
}
