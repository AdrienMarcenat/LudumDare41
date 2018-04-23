using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class GameFlowEndLevelState : FSMState
{
	public delegate void EndLevelEvent (bool pause);

	public static event EndLevelEvent EndLevel;

	[SerializeField] private AudioClip m_SucessMusic;
	private UnityAction<CommandModifier> m_Retry;
	private UnityAction<CommandModifier> m_Nextlevel;
	private UnityAction<CommandModifier> m_Menu;

	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.EndLevel;
		base.Awake ();
		m_Retry = new UnityAction<CommandModifier> (RetryLevel);
		m_Nextlevel = new UnityAction<CommandModifier> (NextLevel);
		m_Menu = new UnityAction<CommandModifier> (GoToMenu);
	}

	public override void Enter ()
	{
		SoundManager.StopMusic ();
		SoundManager.PlaySingle (m_SucessMusic);

		EventManager.Register (PlayerEventManager.Retry, m_Retry);
		EventManager.Register (PlayerEventManager.NextLevel, m_Nextlevel);
		EventManager.Register (PlayerEventManager.Menu, m_Menu);

		StartCoroutine (FreezeTime ());
		if (EndLevel != null)
			EndLevel (true);
	}

	public override void Exit ()
	{
		EventManager.Unregister (PlayerEventManager.Retry, m_Retry);
		EventManager.Unregister (PlayerEventManager.NextLevel, m_Nextlevel);
		EventManager.Unregister (PlayerEventManager.Menu, m_Menu);
		Time.timeScale = 1f;
		if (EndLevel != null)
			EndLevel (false);
	}

	IEnumerator FreezeTime ()
	{
		yield return new WaitForSecondsRealtime (1.5f);
		Time.timeScale = 0f;
	}

	public void RetryLevel (CommandModifier cm)
	{
		requestStateClear ();
		requestStackPush ((int)GameFlowStates.ID.Level);
	}

	public void NextLevel (CommandModifier cm)
	{
		requestStateClear ();
		LevelGenerator.level++;
		requestStackPush ((int)GameFlowStates.ID.Level);
	}

	public void GoToMenu (CommandModifier cm)
	{
		requestStateClear ();
		GameManager.instance.currentScene = 0;
		GameManager.instance.nextState = (int)GameFlowStates.ID.Menu;
		requestStackPush ((int)GameFlowStates.ID.Loading);
	}
}

