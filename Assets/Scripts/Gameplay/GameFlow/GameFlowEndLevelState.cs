using UnityEngine;
using System.Collections;

public class GameFlowEndLevelState : FSMState
{
	public delegate void EndLevelEvent (bool pause);

	public static event EndLevelEvent EndLevel;

	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.EndLevel;
		base.Awake ();
	}

	public override void Enter ()
	{
		Time.timeScale = 0f;
		if (EndLevel != null)
			EndLevel (true);
	}

	public override void Exit ()
	{
		Time.timeScale = 1f;
		if (EndLevel != null)
			EndLevel (false);
	}

	public void NextLevel ()
	{
		requestStateClear ();
		GameManager.instance.currentLevel++;
		GameManager.instance.nextState = (int)GameFlowStates.ID.Level;
		requestStackPush ((int)GameFlowStates.ID.Loading);
	}
}

