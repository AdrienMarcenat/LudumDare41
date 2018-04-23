using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
	public delegate void SimpleEvent ();

	public static event SimpleEvent ChangeScene;

	public int currentScene = 1;
	public int nextState = 0;
	public int currentState = (int)GameFlowStates.ID.Menu;

	public static void LoadScene (int index)
	{
		LevelGenerator.instance.Reset ();
		if (ChangeScene != null)
			ChangeScene ();
		SceneManager.LoadScene (index);
	}

	public static void LoadLevel ()
	{
		LoadScene (instance.currentScene);
	}
}
