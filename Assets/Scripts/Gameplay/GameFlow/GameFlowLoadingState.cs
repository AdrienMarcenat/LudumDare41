using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameFlowLoadingState : FSMState
{
	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.Loading;
		base.Awake ();
	}

	public override void Enter ()
	{
		Time.timeScale = 0f;
		SceneManager.sceneLoaded += OnSceneLoaded;
		GameManager.LoadScene (GameManager.instance.currentScene);
	}

	public override void Exit ()
	{
		Time.timeScale = 1f;
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded (Scene scene, LoadSceneMode mode)
	{
		requestStateClear ();
		requestStackPush (GameManager.instance.nextState);
	}
}

