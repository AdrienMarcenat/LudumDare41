using UnityEngine;
using System.Collections;

public class GameFlowPauseState : FSMState
{
	public static bool pause;

	public delegate void PauseEvent (bool pause);

	public static event PauseEvent Pause;

	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.Pause;
		base.Awake ();
	}

	public override void Enter ()
	{
		TogglePause ();
	}

	public override bool StateUpdate ()
	{
		if (Input.GetButtonDown ("Escape")) {
			Resume ();
		}

		return true;
	}

	public override void Exit ()
	{
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

