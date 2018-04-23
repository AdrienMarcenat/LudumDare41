using UnityEngine;
using System.Collections;

public class GameFlowBossState : FSMState
{
	[SerializeField] private AudioClip m_BossMusic;
	public bool IsBossDead;

	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.Boss;
		base.Awake ();
	}

	public override void Enter ()
	{
		SoundManager.PlayMusic (m_BossMusic);
		IsBossDead = false;
	}

	public override bool StateUpdate ()
	{
		if (Input.GetButtonDown ("Escape")) {
			requestStackPush ((int)GameFlowStates.ID.Pause);
		}
		if (IsBossDead) {
			requestStackPop ();
			requestStackPush ((int)GameFlowStates.ID.Pause);
			requestStackPush ((int)GameFlowStates.ID.EndLevel);
		}

		return true;
	}

	public override void Exit ()
	{
		IsBossDead = false;
	}
}

