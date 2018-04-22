using UnityEngine;
using System.Collections;

public class GameOverPanel : MonoBehaviour
{
	[SerializeField] GameObject gameOverPanel;

	void Start ()
	{
		gameOverPanel.SetActive (false);
	}

	void OnEnable ()
	{
		GameFlowGameOverState.GameOver += GameOver;
	}

	void OnDisable ()
	{
		GameFlowGameOverState.GameOver -= GameOver;
	}

	void GameOver (bool pause)
	{
		gameOverPanel.SetActive (pause);
	}
}

