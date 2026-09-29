using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Incremental Game/Audio Catalog", fileName = "AudioCatalog")]
public class AudioCatalog : ScriptableObject
{
    public List<AudioData> BGMList = new List<AudioData>();
    public List<AudioData> SFXList = new List<AudioData>();

    // 공개 API: 오디오 데이터 검색
    public AudioData GetBGMData(AudioType type)
    {
        return BGMList.Find(data => data.Type == type);
    }

    public AudioData GetSFXData(AudioType type)
    {
        return SFXList.Find(data => data.Type == type);
    }

    private void OnValidate()
    {
        foreach (AudioData data in BGMList)
        {
            data.Category = AudioCategory.BGM;
        }

        foreach (AudioData data in SFXList)
        {
            data.Category = AudioCategory.SFX;
        }
    }
}
