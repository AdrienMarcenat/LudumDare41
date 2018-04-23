using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class GameFlowPauseState : FSMState
{
	public static bool pause;

	public delegate void PauseEvent (bool pause);

	public static event PauseEvent Pause;

	private UnityAction<CommandModifier> m_Resume;
	private UnityAction<CommandModifier> m_Retry;
	private UnityAction<CommandModifier> m_Menu;

	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.Pause;
		base.Awake ();
		m_Menu = new UnityAction<CommandModifier> (GoToMenu);
		m_Resume = new UnityAction<CommandModifier> (Resume);
		m_Retry = new UnityAction<CommandModifier> (RetryLevel);
	}

	public override void Enter ()
	{
		TogglePause ();
		EventManager.Register (PlayerEventManager.Menu, m_Menu);
		EventManager.Register (PlayerEventManager.Resume, m_Resume);
		EventManager.Register (PlayerEventManager.Retry, m_Retry);
	}

	public override bool StateUpdate ()
	{
		if (Input.GetButtonDown ("Escape")) {
			EventManager.TriggerEvent (PlayerEventManager.Resume, new CommandModifier (1, 1, 1));
		}

		return true;
	}

	public override void Exit ()
	{
		EventManager.Unregister (PlayerEventManager.Menu, m_Menu);
		EventManager.Unregister (PlayerEventManager.Resume, m_Resume);
		EventManager.Unregister (PlayerEventManager.Retry, m_Retry);
		TogglePause ();
	}

	private void TogglePause ()
	{
		pause = !pause;
		Time.timeScale = 1.0f - Time.timeScale;
		if (Pause != null)
			Pause (pause);
	}

	public void Resume ()
	{
		requestStackPop ();
	}

	public void Resume (CommandModifier cm)
	{
		requestStackPop ();
	}

	public void RetryLevel (CommandModifier cm)
	{
		requestStateClear ();
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

