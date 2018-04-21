using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class PatternEnemySpawner : MonoBehaviour
{
	[SerializeField] private Transform m_SpawningLocation;
	[SerializeField] private Transform m_ShootDirection;
	[SerializeField] private List<GameObject> m_EnemyPrefabs;
	[SerializeField] private float m_EnemySpawningDelay;
	[SerializeField] private BezierCurve m_Curve;

	private float m_Delay;
	private int currentIndex;

	void Start ()
	{
		m_Delay = m_EnemySpawningDelay;
		currentIndex = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_Delay < m_EnemySpawningDelay)
		{
			m_Delay += Time.deltaTime;
		}
		else
		if (currentIndex < m_EnemyPrefabs.Count)
		{
			m_Delay = 0;
			GameObject enemy = Instantiate (m_EnemyPrefabs [currentIndex]);
			enemy.GetComponent<EnemyAI> ().SetShootDirection (m_ShootDirection);
			enemy.GetComponent<EnemyPath> ().SetPath (m_Curve);
			enemy.transform.position = m_SpawningLocation.position;
			currentIndex++;
		}
		else
		{
			Destroy (this);
		}
	}
}

