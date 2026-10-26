using UnityEngine;

public class OfflineManager : MonoBehaviour
{
	[SerializeField, Min(0)] private float maxOfflineHours = 8;
	[SerializeField, Range(0, 1)] private float offlineEfficiency = 0.5f;

	public double GetElapsedSeconds(long savedUtcTicks)
	{
		return OfflineSystem.GetElapsedSeconds(savedUtcTicks);
	}

	public double CalculateReward(double seconds, double goldPerSecond)
	{
		return OfflineSystem.CalculateReward(seconds, goldPerSecond, maxOfflineHours, offlineEfficiency);
	}
}