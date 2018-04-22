using UnityEngine;
using System.Collections;

public class GameFlowBossState : FSMState
{
	public bool IsBossDead;

	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.Boss;
		base.Awake ();
	}

	public override void Enter ()
	{
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

