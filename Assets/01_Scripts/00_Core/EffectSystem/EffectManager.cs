using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    [SerializeField] private EffectCatalog effectCatalog;

    private readonly Dictionary<EffectType, Queue<EffectObject>> pools = new Dictionary<EffectType, Queue<EffectObject>>();

    // 공개 API: 이펙트 재생
    public void Play(EffectType type, Vector3 position)
    {
        Play(type, position, Quaternion.identity, null);
    }

    public void Play(EffectType type, Vector3 position, Quaternion rotation)
    {
        Play(type, position, rotation, null);
    }

    public void Play(EffectType type, Vector3 position, object value)
    {
        Play(type, position, Quaternion.identity, value);
    }

    public void Play(EffectType type, Vector3 position, Quaternion rotation, object value)
    {
        EffectData data = effectCatalog.GetEffectData(type);
        Queue<EffectObject> pool = pools[type];
        EffectObject effectObject = pool.Count > 0 ? pool.Dequeue() : CreateEffect(data);

        effectObject.transform.SetPositionAndRotation(position, rotation);
        effectObject.Play(data.Duration, value);
    }

    // 공개 API: 풀 반환
    public void Return(EffectObject effectObject)
    {
        effectObject.gameObject.SetActive(false);
        pools[effectObject.Type].Enqueue(effectObject);
    }

    // Unity 생명주기
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        PrewarmPools();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    // 내부 풀 관리
    private void PrewarmPools()
    {
        foreach (EffectData data in effectCatalog.Effects)
        {
            Queue<EffectObject> pool = new Queue<EffectObject>();
            pools.Add(data.Type, pool);

            for (int i = 0; i < data.PoolSize; i++)
            {
                pool.Enqueue(CreateEffect(data));
            }
        }
    }

    private EffectObject CreateEffect(EffectData data)
    {
        GameObject instance = Instantiate(data.Prefab, transform);
        EffectObject effectObject = instance.GetComponent<EffectObject>();
        effectObject.Initialize(this, data.Type);
        return effectObject;
    }
}
