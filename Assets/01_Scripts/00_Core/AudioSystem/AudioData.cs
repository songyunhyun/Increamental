using System;
using UnityEngine;

[Serializable]
public class AudioData
{
    public AudioType Type;
    public AudioCategory Category;
    public AudioClip Clip;
    [Range(0f, 1f)] public float Volume = 1f;
    public bool Loop;
}
