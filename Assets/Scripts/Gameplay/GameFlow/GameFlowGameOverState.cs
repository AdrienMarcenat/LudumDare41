using UnityEngine;
using System.Collections;

public class GameFlowGameOverState : FSMState
{
	public delegate void GameOverEvent (bool pause);

	public static event GameOverEvent GameOver;

	[SerializeField] private AudioClip m_GameOverMusic;

	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.GameOver;
		base.Awake ();
	}

	public override void Enter ()
	{
		SoundManager.PlayMusic (m_GameOverMusic);
		Time.timeScale = 0f;
		if (GameOver != null)
			GameOver (true);
	}

	public override void Exit ()
	{
		Time.timeScale = 1f;
		if (GameOver != null)
			GameOver (false);
	}

	public void RetryLevel ()
	{
		requestStateClear ();
		GameManager.instance.nextState = (int)GameFlowStates.ID.Level;
		requestStackPush ((int)GameFlowStates.ID.Loading);
	}

	public void GoToMenu ()
	{
		requestStateClear ();
		GameManager.instance.nextState = (int)GameFlowStates.ID.Menu;
		requestStackPush ((int)GameFlowStates.ID.Loading);
	}
}

