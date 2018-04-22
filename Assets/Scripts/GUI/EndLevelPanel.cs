using UnityEngine;
using System.Collections;

public class EndLevelPanel : MonoBehaviour
{
	[SerializeField] GameObject endLevelPanel;

	void Start ()
	{
		endLevelPanel.SetActive (false);
	}

	void OnEnable ()
	{
		GameFlowEndLevelState.EndLevel += EndLevel;
	}

	void OnDisable ()
	{
		GameFlowEndLevelState.EndLevel -= EndLevel;
	}

	void EndLevel (bool pause)
	{
		endLevelPanel.SetActive (pause);
	}
}

