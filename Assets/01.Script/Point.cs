using UnityEngine;

public class Point : MonoBehaviour
{
    public void PlusPoint()
    {
        GoldManager.Instance.AddGold(1);
    }
}
