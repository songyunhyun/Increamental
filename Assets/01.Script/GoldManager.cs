using System;
using UnityEngine;

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
    public void AddGold(int amount)
    {
        Gold += amount;
        OnUpdateGold?.Invoke(Gold);
    }
}
