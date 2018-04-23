using UnityEngine;
using System.Collections;

public class WaitingTrigger : MonoBehaviour
{
	private LevelGenerator m_LevelManager;

	void Start ()
	{
		m_LevelManager = GameObject.Find ("LevelManager").GetComponent<LevelGenerator> ();
	}

	void OnDisable ()
	{
		if (m_LevelManager)
			m_LevelManager.isWaiting = false;
	}
}

