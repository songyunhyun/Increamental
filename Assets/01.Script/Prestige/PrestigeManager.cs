using UnityEngine;
public class PrestigeManager : MonoBehaviour
{
    public static PrestigeManager Instance;
    public int PrestigeCount { get; private set;  }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }


}
