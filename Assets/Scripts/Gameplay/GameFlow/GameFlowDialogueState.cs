using UnityEngine;
using System.Collections;

public class GameFlowDialogueState : FSMState
{
	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.Dialogue;
		base.Awake ();
	}

	public override void Enter ()
	{
		
	}

	public override bool StateUpdate ()
	{
		if (Input.GetButtonDown ("Escape")) {
			requestStackPush ((int)GameFlowStates.ID.Pause);
		}

		return true;
	}

	public override void Exit ()
	{

	}
}

