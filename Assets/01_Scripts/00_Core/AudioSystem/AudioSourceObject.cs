using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceObject : MonoBehaviour
{
    private AudioManager manager;
    private AudioSource audioSource;

    // 공개 API: SFX 소스 초기화와 재생
    public void Initialize(AudioManager audioManager, AudioSource source)
    {
        manager = audioManager;
        audioSource = source;
        gameObject.SetActive(false);
    }

    public void Play(AudioData data)
    {
        gameObject.SetActive(true);
        audioSource.clip = data.Clip;
        audioSource.volume = data.Volume;
        audioSource.loop = data.Loop;
        audioSource.Play();
        StartCoroutine(ReturnWhenFinished());
    }

    private IEnumerator ReturnWhenFinished()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        manager.Return(this);
    }
}
