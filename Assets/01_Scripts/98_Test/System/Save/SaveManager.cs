using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	[SerializeField] private AutomationManager automationManager;
	[SerializeField] private OfflineManager offlineManager;

	private SaveData data = new SaveData();
	private float saveTimer;
	private bool paused;

	public double Gold => data.gold;
	public int UpgradeCount => data.upgradeCount;
	public double GoldPerSecond => automationManager.GoldPerSecond;
	public double LastOfflineReward { get; private set; }
	public string SavePath => Path.Combine(Application.persistentDataPath, "offline-lesson.json");

	public void Load()
	{
		LoadProgress();
	}

	public void Save()
	{
		SaveProgress();
	}

	public void Upgrade()
	{
		UpgradeProduction();
		SaveProgress();
	}

	public double CalculateOfflineReward(double seconds)
	{
		return offlineManager.CalculateReward(seconds, GoldPerSecond);
	}

	public void ReceiveOfflineReward(double seconds)
	{
		AddOfflineReward(seconds);
		SaveProgress();
	}

	private void Awake()
	{
		LoadProgress();
	}

	private void Update()
	{
		if (paused)
		{
			return;
		}

		automationManager.Produce(Time.unscaledDeltaTime);
		UpdateAutoSave(Time.unscaledDeltaTime);
	}

	private void LoadProgress()
	{
		try
		{
			data = SaveSystem.Load(SavePath);
			ValidateData();
			automationManager.Initialize(data);
			double seconds = offlineManager.GetElapsedSeconds(data.savedUtcTicks);
			AddOfflineReward(seconds);
			SaveProgress();
		}
		catch (Exception error)
		{
			Debug.LogError("저장 파일을 읽지 못했습니다: " + error.Message, this);
			enabled = false;
		}
	}

	private void ValidateData()
	{
		bool invalidGold = double.IsNaN(data.gold) || double.IsInfinity(data.gold) || data.gold < 0;
		bool invalidUpgrade = data.upgradeCount < 0;
		bool invalidTime = data.savedUtcTicks < 0 || data.savedUtcTicks > DateTime.MaxValue.Ticks;
		bool invalidData = invalidGold || invalidUpgrade || invalidTime;

		if (invalidData)
		{
			throw new InvalidDataException("저장된 값이 올바르지 않습니다.");
		}
	}

	private void SaveProgress()
	{
		try
		{
			data.savedUtcTicks = DateTime.UtcNow.Ticks;
			SaveSystem.Save(SavePath, data);
			saveTimer = 0;
		}
		catch (Exception error)
		{
			Debug.LogError("저장하지 못했습니다: " + error.Message, this);
		}
	}

	private void UpgradeProduction()
	{
		automationManager.Upgrade();
	}

	private void AddOfflineReward(double seconds)
	{
		LastOfflineReward = offlineManager.CalculateReward(seconds, GoldPerSecond);
		data.gold += LastOfflineReward;
	}

	private void UpdateAutoSave(float seconds)
	{
		saveTimer += seconds;

		if (saveTimer >= 10)
		{
			SaveProgress();
		}
	}

	private void OnApplicationPause(bool isPaused)
	{
		bool ignorePause = !enabled || paused == isPaused;

		if (ignorePause)
		{
			return;
		}

		paused = isPaused;

		if (isPaused)
		{
			SaveProgress();
		}
		else
		{
			LoadProgress();
		}
	}

	private void OnApplicationQuit()
	{
		bool canSave = enabled && !paused;

		if (canSave)
		{
			SaveProgress();
		}
	}
}