using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] AudioData gameMusic;

	[SerializeField] GameObject playerPrefab;
	[SerializeField] Transform playerStart;

	[Header("Events")]
	[SerializeField] EventRouter startGameEvent;
	[SerializeField] EventRouter stopGameEvent;
	[SerializeField] EventRouter winGameEvent;


	private AudioSourceController gameMusicSource;

	public enum State
	{
		TITLE,
		START_GAME,
		START_LEVEL,
		PLAY_GAME,
		PLAYER_DEAD,
		GAME_OVER
	}

	State state = State.START_GAME;
	float stateTimer = 0;
	int lives = 0;


	private void Start()
	{
		winGameEvent.onEvent += SetGameWin;
		gameMusicSource = gameMusic.Play();
		gameMusicSource.Stop();
	}

	private void Update()
	{
		switch (state)
		{
			case State.TITLE:
				UIManager.Instance.ShowTitle(true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				break;
			case State.START_GAME:
				UIManager.Instance.ShowTitle(false);
				Cursor.lockState = CursorLockMode.Locked;
				lives = 3;
				//UIManager.Instance.SetLivesUI(lives);
				state = State.START_LEVEL;
				break;
			case State.START_LEVEL:
				startGameEvent.Notify();
				gameMusicSource.Play();
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
				UIManager.Instance.ShowGameOver(true);
				stateTimer -= Time.deltaTime;
				if (stateTimer <= 0)
				{
					UIManager.Instance.ShowGameOver(false);
					state = State.TITLE;
				}
				break;
			default:
				break;
		}
	}



	public void SetPlayerDead()
	{
		stopGameEvent.Notify();
		gameMusicSource.Stop();

		lives--;
		UIManager.Instance.SetLivesUI(lives);
		state = (lives == 0) ? State.GAME_OVER : State.PLAYER_DEAD;
		stateTimer = 3;
	}

	public void SetGameOver()
	{
		stopGameEvent.Notify();
		UIManager.Instance.ShowGameOver(true);
		gameMusicSource.Stop();
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
