using UnityEngine;


public abstract class AbstractUpgrade : MonoBehaviour
{
    [SerializeField] private int _upgradeCost;
    public int CurrentValue { get; private set; } = 1;
    public void UpgradeArrow()
    {
        if (CanUpgrade())
        {
            CurrentValue++;
            GoldManager.Instance.AddGold(-_upgradeCost);
            _upgradeCost += Mathf.Max(1, _upgradeCost / 5);
        }
    }

    private bool CanUpgrade()
    {
        return _upgradeCost >= GoldManager.Instance.Gold;
    }
}
