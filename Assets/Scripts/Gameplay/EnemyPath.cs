﻿using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System;
using UnityEngine.Analytics;

public class EnemyPath : Enemy
{
	[SerializeField] private BezierCurve m_Path;
	[SerializeField] private float m_Duration;

	private float m_Progress;

	private void Update ()
	{
		m_Progress += Time.deltaTime / m_Duration;
		if (m_Progress > 1f)
		{
			m_Progress = 1f;
		}
		Vector2 position = m_Path.GetPoint (m_Progress);
		transform.localPosition = position;
	}
}
