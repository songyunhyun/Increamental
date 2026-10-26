using System;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public static HPManager Instance;
    private const int originHP = 10;
    [field:SerializeField]public int HP { get; set; }
    public event Action<int> OnUpdateHealth;
    public event Action OnDeath;
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
        ResetHP();
    }
    public void ResetHP()
    {
        HP = originHP;
        OnUpdateHealth?.Invoke(HP);
    }

    public void HealthUpdate(int amount)
    {
        HP += amount;
        OnUpdateHealth?.Invoke(HP);
        if (HP <= 0)
            Death();
    }

    public void SetHP(int value)
    {
        HP = value;
        OnUpdateHealth?.Invoke(HP);
    }

    private void Death()
    {
        OnDeath?.Invoke();
        print("잉힝힝 주거써");
    }
}
