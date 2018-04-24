using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Analytics;
using System.IO;

public class EnemyPath : Enemy
{
	[SerializeField] private BezierCurve m_Path;
	[SerializeField] private float m_Duration;
	[SerializeField] private bool m_Loop;

	private float m_Progress;

	private void Update ()
	{
		m_Progress += Time.deltaTime / m_Duration;
		if (m_Progress > 1f) {
			if (m_Loop) {
				m_Progress = 0f;
			} else {
				Destroy (gameObject);
			}
		}
		Vector2 position = m_Path.GetPoint (m_Progress);
		transform.localPosition = position;
	}

	public void SetPath (BezierCurve path)
	{
		m_Path = path;
	}

	public void SetLoop (bool loop)
	{
		m_Loop = loop;
	}

	public void SetDuration (float duration)
	{
		m_Duration = duration;
	}
}

