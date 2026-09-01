using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;
    public int Gold { get; private set; }
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
        if(Gold<2000000000)
            Gold += amount;
        else
        {

        }
    }
}
