using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public AudioType audioType;
    public bool isSFX;

    public void Start()
    {
        if (isSFX)
        {
            AudioManager.Instance.PlaySFX(audioType);
        }
        else
        {
            AudioManager.Instance.PlayBGM(audioType);
        }
    }
}
