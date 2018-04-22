using UnityEngine;
using System.Collections;

public class PausePanel : MonoBehaviour
{
	[SerializeField] GameObject pausePanel;

	void Start ()
	{
		pausePanel.SetActive (false);
	}

	void OnEnable ()
	{
		GameFlowPauseState.Pause += Pause;
	}

	void OnDisable ()
	{
		GameFlowPauseState.Pause -= Pause;
	}

	void Pause (bool pause)
	{
		pausePanel.SetActive (pause);
	}
}

