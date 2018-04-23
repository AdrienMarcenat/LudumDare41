using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class GameFlowGameOverState : FSMState
{
	public delegate void GameOverEvent (bool pause);

	public static event GameOverEvent GameOver;

	[SerializeField] private AudioClip m_GameOverMusic;

	private UnityAction<CommandModifier> m_Menu;
	private UnityAction<CommandModifier> m_Retry;

	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.GameOver;
		base.Awake ();
		m_Menu = new UnityAction<CommandModifier> (GoToMenu);
		m_Retry = new UnityAction<CommandModifier> (RetryLevel);
	}

	public override void Enter ()
	{
		SoundManager.PlayMusic (m_GameOverMusic);
		StartCoroutine (FreezeTime ());
		if (GameOver != null)
			GameOver (true);
		EventManager.Register (PlayerEventManager.Menu, m_Menu);
		EventManager.Register (PlayerEventManager.Retry, m_Retry);
	}

	public override void Exit ()
	{
		EventManager.Unregister (PlayerEventManager.Menu, m_Menu);
		EventManager.Unregister (PlayerEventManager.Retry, m_Retry);
		Time.timeScale = 1f;
		if (GameOver != null)
			GameOver (false);
	}

	IEnumerator FreezeTime ()
	{
		yield return new WaitForSecondsRealtime (1.5f);
		Time.timeScale = 0f;
	}

	public void RetryLevel (CommandModifier cm)
	{
		requestStateClear ();
		GameManager.instance.nextState = (int)GameFlowStates.ID.Level;
		requestStackPush ((int)GameFlowStates.ID.Loading);
	}

	public void GoToMenu (CommandModifier cm)
	{
		requestStateClear ();
		GameManager.instance.currentLevel = 0;
		GameManager.instance.nextState = (int)GameFlowStates.ID.Menu;
		requestStackPush ((int)GameFlowStates.ID.Loading);
	}
}

