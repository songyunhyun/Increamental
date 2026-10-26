using System;
using UnityEngine;
using static UnityEngine.UI.Image;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;
    [field:SerializeField]public int Gold { get; private set; }
    public event Action<int> OnUpdateGold;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ResetGold()
    {
        Gold = 0;
        OnUpdateGold?.Invoke(Gold);
    }
    public void SetGold(int value)
    {
        Gold = value;
        OnUpdateGold?.Invoke(Gold);
    }
    public void AddGold(int amount)
    {
        Gold += amount;
        OnUpdateGold?.Invoke(Gold);
    }
}
