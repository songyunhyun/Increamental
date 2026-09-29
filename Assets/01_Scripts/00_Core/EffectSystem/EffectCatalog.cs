using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Incremental Game/Effect Catalog", fileName = "EffectCatalog")]
public class EffectCatalog : ScriptableObject
{
    public List<EffectData> Effects = new List<EffectData>();

    public EffectData GetEffectData(EffectType type)
    {
        return Effects.Find(data => data.Type == type);
    }
}
