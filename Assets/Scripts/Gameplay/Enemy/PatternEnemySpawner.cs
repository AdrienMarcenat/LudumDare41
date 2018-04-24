using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatternEnemySpawner : MonoBehaviour
{
	[SerializeField] private List<GameObject> m_EnemyPrefabs;
	[SerializeField] private float m_EnemySpawningDelay;

	private float m_Delay;
	private int currentIndex;

	void Start ()
	{
		m_Delay = m_EnemySpawningDelay;
		currentIndex = 0;
	}

	void Update ()
	{
		if (m_Delay < m_EnemySpawningDelay) {
			m_Delay += Time.deltaTime;
		} else if (currentIndex < m_EnemyPrefabs.Count) {
			m_Delay = 0;
			Instantiate (m_EnemyPrefabs [currentIndex]);
			currentIndex++;
		} else {
			Destroy (this);
		}
	}
}

