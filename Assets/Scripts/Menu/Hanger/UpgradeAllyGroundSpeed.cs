﻿using UnityEngine;
using System.Collections;

public class UpgradeAllyGroundSpeed : MonoBehaviour
{
	[SerializeField] UpgradesContainer upgradesContainer;
	[SerializeField] UILabel upgradeNameLabel;
	[SerializeField] UILabel upgradeCostLabel;
	[SerializeField] UISlider upgradeSlider;
	[SerializeField] Collider buttonCollider;
	[SerializeField] UILabel rewardPointsLabel;

	void OnEnable()
	{
		transform.parent.gameObject.BroadcastMessage("UpdateLabels");
	}

	void OnClick()
	{
		if (PlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.AllyGroundSpeedUpgradeCost)
			UpgradeSpeed();
		else
			Debug.Log("Not enough RP for upgrade purchase....");
	}

	void UpdateLabels()
	{
		if (upgradesContainer.AllyGroundSpeedLevel < 5)
			upgradeCostLabel.text = upgradesContainer.AllyGroundSpeedUpgradeCost.ToString("N0") + " RP";
		else
		{
			upgradeCostLabel.text = "Upgrade Full";
			upgradesContainer.AllyGroundSpeedUpgradeCost = 0;
			buttonCollider.enabled = false;
		}

		upgradeSlider.value = ((PlayerPrefs.GetInt("Ally Ground Speed Level", 0) * 1.0f) + 1) / 6f;
		rewardPointsLabel.text = PlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
		
		if (PlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.AllyGroundSpeedUpgradeCost)
		{
			upgradeNameLabel.color = Color.white;
			upgradeCostLabel.color = Color.white;
		}
		else
		{
			upgradeNameLabel.color = Color.red;
			upgradeCostLabel.color = Color.red;
		}
	}

	void UpgradeSpeed()
	{
		float currentRP = PlayerPrefs.GetFloat("Reward Points", 0);
		float newRP = currentRP - upgradesContainer.AllyGroundSpeedUpgradeCost;
		PlayerPrefs.SetFloat("Reward Points", newRP);

		int currentLevel = PlayerPrefs.GetInt("Ally Ground Speed Level", 0);
		int newLevel = currentLevel + 1;
		PlayerPrefs.SetInt("Ally Ground Speed Level", newLevel);

		switch(newLevel)
		{
		case 1:
			PlayerPrefs.SetFloat("Ally Ground Speed Multiplier", 1.2f);
			break;
		case 2:
			PlayerPrefs.SetFloat("Ally Ground Speed Multiplier", 1.4f);
			break;
		case 3:
			PlayerPrefs.SetFloat("Ally Ground Speed Multiplier", 1.6f);
			break;
		case 4:
			PlayerPrefs.SetFloat("Ally Ground Speed Multiplier", 1.8f);
			break;
		case 5:
			PlayerPrefs.SetFloat("Ally Ground Speed Multiplier", 2.0f);
			break;
		default:
			Debug.LogError("Selection Not Valid: " + transform.name);
			break;
		}

		PlayerPrefs.Save();
		upgradesContainer.UpgradeAllyGroundSpeedLevel();
		transform.parent.gameObject.BroadcastMessage("UpdateLabels");
	}
}