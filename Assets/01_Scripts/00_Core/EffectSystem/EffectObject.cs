using System.Collections;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    public EffectType Type { get; private set; }

    private EffectManager manager;

    public void Initialize(EffectManager effectManager, EffectType type)
    {
        manager = effectManager;
        Type = type;
        gameObject.SetActive(false);
    }

    // 공개 API: 이펙트 재생
    public void Play(float duration)
    {
        Play(duration, null);
    }

    public virtual void Play(float duration, object value)
    {
        gameObject.SetActive(true);
        StartCoroutine(ReturnAfter(duration));
    }

    private IEnumerator ReturnAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        manager.Return(this);
    }
}
