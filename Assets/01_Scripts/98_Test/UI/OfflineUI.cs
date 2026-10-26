using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OfflineUI : MonoBehaviour
{
	[FormerlySerializedAs("system")]
	[SerializeField] private SaveManager saveManager;
	[SerializeField] private TMP_Text goldText;
	[SerializeField] private TMP_Text upgradeText;
	[SerializeField] private TMP_Text productionText;
	[SerializeField] private TMP_Text rewardText;
	[SerializeField] private TMP_Text currentTimeText;
	[SerializeField] private TMP_Text nextTimeText;
	[SerializeField] private TMP_InputField minutesInput;
	[SerializeField] private Button upgradeButton;
	[SerializeField] private Button editButton;
	[SerializeField] private Button applyButton;

	private double previewSeconds;

	public void PreviewTime()
	{
		UpdatePreview();
	}

	public void ApplyTime()
	{
		ApplyPreview();
	}

	private void Start()
	{
		if (!saveManager.enabled)
		{
			DisableButtons();
			enabled = false;
			return;
		}

		ConnectButtons();
		currentTimeText.text = "현재 시간 : " + DateTime.Now.ToString("MM/dd HH:mm:ss");
		CancelPreview("");
	}

	private void Update()
	{
		RefreshText();
	}

	private void ConnectButtons()
	{
		upgradeButton.onClick.AddListener(saveManager.Upgrade);
		editButton.onClick.AddListener(PreviewTime);
		applyButton.onClick.AddListener(ApplyTime);
		minutesInput.onValueChanged.AddListener(CancelPreview);
	}

	private void DisableButtons()
	{
		upgradeButton.interactable = false;
		editButton.interactable = false;
		applyButton.interactable = false;
	}

	private void UpdatePreview()
	{
		bool parsed = double.TryParse(minutesInput.text, out double minutes);
		bool invalidNumber = double.IsNaN(minutes) || double.IsInfinity(minutes);
		bool outsideRange = minutes <= 0 || minutes > 525600;
		bool invalidInput = !parsed || invalidNumber || outsideRange;

		if (invalidInput)
		{
			CancelPreview("");
			nextTimeText.text = "이후 시간 : 1~525600 min 입력";
			return;
		}

		previewSeconds = minutes * 60;
		DateTime now = DateTime.Now;
		currentTimeText.text = "현재 시간 : " + now.ToString("MM/dd HH:mm:ss");
		nextTimeText.text = "이후 시간 : " + now.AddMinutes(minutes).ToString("MM/dd HH:mm:ss");
		applyButton.interactable = true;
	}

	private void ApplyPreview()
	{
		if (previewSeconds <= 0)
		{
			return;
		}

		saveManager.ReceiveOfflineReward(previewSeconds);
		CancelPreview("");
	}

	private void CancelPreview(string value)
	{
		previewSeconds = 0;
		applyButton.interactable = false;
		nextTimeText.text = "이후 시간 : -";
	}

	private void RefreshText()
	{
		goldText.text = saveManager.Gold.ToString("N1") + " G";
		upgradeText.text = "강화 " + saveManager.UpgradeCount + "회";
		productionText.text = "+" + saveManager.GoldPerSecond.ToString("N1") + " G / 초";
		rewardText.text = "+" + saveManager.LastOfflineReward.ToString("N1") + " G";
	}

	private void OnDestroy()
	{
		upgradeButton.onClick.RemoveListener(saveManager.Upgrade);
		editButton.onClick.RemoveListener(PreviewTime);
		applyButton.onClick.RemoveListener(ApplyTime);
		minutesInput.onValueChanged.RemoveListener(CancelPreview);
	}
}