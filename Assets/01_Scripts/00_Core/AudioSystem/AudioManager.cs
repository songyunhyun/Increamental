using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioCatalog audioCatalog;

    // AudioMixer
    [SerializeField] private AudioMixer audioMixer;
    private AudioSource bgmSource;
    private AudioMixerGroup sfxMixerGroup;
    private const string MasterVolumeParameter = "Master";
    private const string BGMVolumeParameter = "BGM";
    private const string SFXVolumeParameter = "SFX";

    // SFX 풀
    [SerializeField, Min(0)] private int sfxPoolSize = 10;
    private readonly Queue<AudioSourceObject> sfxPool = new Queue<AudioSourceObject>();

    // 공개 API: BGM 재생
    public void PlayBGM(AudioType type)
    {
        AudioData data = audioCatalog.GetBGMData(type);

        bgmSource.clip = data.Clip;
        bgmSource.volume = data.Volume;
        bgmSource.loop = data.Loop;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
        bgmSource.clip = null;
    }

    // 공개 API: SFX 재생과 반환
    public void PlaySFX(AudioType type)
    {
        AudioData data = audioCatalog.GetSFXData(type);

        AudioSourceObject sourceObject = sfxPool.Count > 0 ? sfxPool.Dequeue() : CreateSFXSource();
        sourceObject.Play(data);
    }

    public void Return(AudioSourceObject sourceObject)
    {
        sourceObject.gameObject.SetActive(false);
        sfxPool.Enqueue(sourceObject);
    }

    // 공개 API: 볼륨 조절
    public void SetMasterVolume(float volume)
    {
        SetVolume(MasterVolumeParameter, volume);
    }

    public void SetBGMVolume(float volume)
    {
        SetVolume(BGMVolumeParameter, volume);
    }

    public void SetSFXVolume(float volume)
    {
        SetVolume(SFXVolumeParameter, volume);
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
        CreateBGMSource();
        PrewarmSFXPool();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    // 내부 AudioSource 준비
    private void CreateBGMSource()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.playOnAwake = false;
        bgmSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        sfxMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
    }

    private void PrewarmSFXPool()
    {
        for (int i = 0; i < sfxPoolSize; i++)
        {
            sfxPool.Enqueue(CreateSFXSource());
        }
    }

    private AudioSourceObject CreateSFXSource()
    {
        GameObject sourceGameObject = new GameObject("SFX AudioSource");
        sourceGameObject.transform.SetParent(transform);

        AudioSource source = sourceGameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.outputAudioMixerGroup = sfxMixerGroup;

        AudioSourceObject sourceObject = sourceGameObject.AddComponent<AudioSourceObject>();
        sourceObject.Initialize(this, source);
        return sourceObject;
    }

    // 내부 볼륨 계산
    private void SetVolume(string parameter, float volume)
    {
        float decibel = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(parameter, decibel);
    }
}
