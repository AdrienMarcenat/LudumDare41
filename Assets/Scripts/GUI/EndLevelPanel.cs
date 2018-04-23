using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndLevelPanel : MonoBehaviour
{
	[SerializeField] GameObject endLevelPanel;
	[SerializeField] Text m_ScoreText;

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
		m_ScoreText.text = "Score : " + GameManager.score;
	}
}

