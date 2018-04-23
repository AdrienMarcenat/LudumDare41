using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour
{
	[SerializeField] Text m_ScoreText;

	void Update ()
	{
		m_ScoreText.text = "Score : " + GameManager.score;
	}

}

