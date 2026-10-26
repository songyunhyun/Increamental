using UnityEngine;

public class AutomationManager : MonoBehaviour
{
	[SerializeField, Min(0)] private float baseGoldPerSecond = 1;
	[SerializeField, Min(0)] private float goldPerUpgrade = 1;
	private SaveData data;

	public double GoldPerSecond => AutomationSystem.CalculateRate(data.upgradeCount, baseGoldPerSecond, goldPerUpgrade);

	public void Initialize(SaveData saveData)
	{
		data = saveData;
	}

	public void Upgrade()
	{
		AddUpgrade();
	}

	public void Produce(double seconds)
	{
		AddProducedGold(seconds);
	}

	private void AddUpgrade()
	{
		data.upgradeCount++;
	}

	private void AddProducedGold(double seconds)
	{
		double gold = AutomationSystem.CalculateGold(GoldPerSecond, seconds);
		data.gold += gold;
	}
}