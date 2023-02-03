using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RollerGameManager : Singleton<RollerGameManager>
{
	[SerializeField] Slider healthMeter;
	[SerializeField] TMP_Text scoreUI;
	[SerializeField] TMP_Text livesUI;
	[SerializeField] GameObject gameOverUI;
	[SerializeField] GameObject titleUI;

	[SerializeField] AudioSource gameMusic;

	[SerializeField] GameObject playerPrefab;
	[SerializeField] Transform playerStart;

	[Header("Events")]
	[SerializeField] EventRouter startGameEvent;
	[SerializeField] EventRouter stopGameEvent;
	[SerializeField] EventRouter winGameEvent;


	public enum State
	{
		TITLE,
		START_GAME,
		START_LEVEL,
		PLAY_GAME,
		PLAYER_DEAD,
		GAME_OVER
	}

	State state = State.TITLE;
	float stateTimer = 0;
	int lives = 0;


	private void Start()
	{
		winGameEvent.onEvent += SetGameWin;
	}

	private void Update()
	{
		switch (state)
		{
			case State.TITLE:
				titleUI.SetActive(true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				break;
			case State.START_GAME:
				titleUI.SetActive(false);
				Cursor.lockState = CursorLockMode.Locked;
				lives = 3;
				SetLivesUI(lives);
				state = State.START_LEVEL;
				break;
			case State.START_LEVEL:
				startGameEvent.Notify();
				gameMusic.Play();
				Instantiate(playerPrefab, playerStart.position, playerStart.rotation);
				state = State.PLAY_GAME;
				break;
			case State.PLAY_GAME:
				//
				break;
			case State.PLAYER_DEAD:
				stateTimer -= Time.deltaTime;
				if (stateTimer <= 0)
				{
					state = State.START_LEVEL;
				}
				break;
			case State.GAME_OVER:
				gameOverUI.SetActive(true);
				stateTimer -= Time.deltaTime;
				if (stateTimer <= 0)
				{
					gameOverUI.SetActive(false);
					state = State.TITLE;
				}
				break;
			default:
				break;
		}
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

	public void SetPlayerDead()
	{
		stopGameEvent.Notify();
		gameMusic.Stop();

		lives--;
		SetLivesUI(lives);
		state = (lives == 0) ? State.GAME_OVER : State.PLAYER_DEAD;
		stateTimer = 3;
	}

	public void SetGameOver()
	{
		stopGameEvent.Notify();
		gameOverUI.SetActive(true);
		gameMusic.Stop();
		state = State.GAME_OVER;
		stateTimer = 3;
	}

	public void SetGameWin()
	{
		Debug.Log("Win!!!");
	}

	public void OnStartGame()
	{
		state = State.START_GAME;
	}

}
