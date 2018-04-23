using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class GameFlowLevelState : FSMState
{
	[SerializeField] private AudioClip m_LevelMusic;

	private UnityAction<CommandModifier> m_ActionPause;

	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.Level;
		base.Awake ();
		m_ActionPause = new UnityAction<CommandModifier> (Pause);
	}

	public override void Enter ()
	{
		EventManager.Register (PlayerEventManager.Pause, m_ActionPause);
		SoundManager.PlayMusic (m_LevelMusic);
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ().GameOver += GameOver;
	}

	public override bool StateUpdate ()
	{
		if (Input.GetButtonDown ("Escape")) {
			EventManager.TriggerEvent (PlayerEventManager.Pause, new CommandModifier (1, 1, 1));
		}

		return true;
	}

	public override void Exit ()
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ().GameOver -= GameOver;
		EventManager.Unregister (PlayerEventManager.Pause, m_ActionPause);
	}

	private void Pause (CommandModifier cm)
	{
		requestStackPush ((int)GameFlowStates.ID.Pause);
	}

	private void Boss ()
	{
		requestStackPop ();
		requestStackPush ((int)GameFlowStates.ID.Boss);
	}

	private void GameOver ()
	{
		requestStackPop ();
		requestStackPush ((int)GameFlowStates.ID.GameOver);
	}
}

