using UnityEngine;
using System.Collections;

public class PlayerGameOverState : FSMState
{
	protected override void Awake ()
	{
		ID = (int)PlayerStates.ID.GameOver;
		base.Awake ();
	}

	public override void Enter ()
	{
		
	}

	public override void Exit ()
	{
		
	}
}

