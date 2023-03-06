using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
	[SerializeField] Slider healthMeter;
	[SerializeField] TMP_Text scoreUI;
	[SerializeField] TMP_Text livesUI;
	[SerializeField] GameObject gameOverUI;
	[SerializeField] GameObject titleUI;

	public void ShowTitle(bool show = true)
	{
		titleUI.SetActive(show);
	}

	public void ShowGameOver(bool show = true)
	{
		gameOverUI.SetActive(show);
	}


	public void SetHealth(int health)
	{
		healthMeter.value = Mathf.Clamp(health, 0, 100);
	}

	public void SetScore(int score)
	{
		scoreUI.text = score.ToString();
	}

	public void SetLivesUI(int lives)
	{
		livesUI.text = "Lives: " + lives.ToString();
	}

}
