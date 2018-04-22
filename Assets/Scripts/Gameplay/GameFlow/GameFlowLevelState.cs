using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;

public class GameFlowLevelState : FSMState
{
	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.Level;
		base.Awake ();
	}

	public override void Enter ()
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ().GameOver += GameOver;
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
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ().GameOver -= GameOver;
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

