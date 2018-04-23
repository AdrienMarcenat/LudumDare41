using UnityEngine;
using System.Collections;

public class GameFlowEndLevelState : FSMState
{
	public delegate void EndLevelEvent (bool pause);

	public static event EndLevelEvent EndLevel;

	[SerializeField] private AudioClip m_SucessMusic;

	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.EndLevel;
		base.Awake ();
	}

	public override void Enter ()
	{
		SoundManager.StopMusic ();
		SoundManager.PlaySingle (m_SucessMusic);
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

	public void RetryLevel ()
	{
		requestStateClear ();
		GameManager.instance.nextState = (int)GameFlowStates.ID.Level;
		requestStackPush ((int)GameFlowStates.ID.Loading);
	}

	public void NextLevel ()
	{
		requestStateClear ();
		GameManager.instance.currentLevel++;
		GameManager.instance.nextState = (int)GameFlowStates.ID.Level;
		requestStackPush ((int)GameFlowStates.ID.Loading);
	}

	public void GoToMenu ()
	{
		requestStateClear ();
		GameManager.instance.currentLevel = 0;
		GameManager.instance.nextState = (int)GameFlowStates.ID.Menu;
		requestStackPush ((int)GameFlowStates.ID.Loading);
	}
}

