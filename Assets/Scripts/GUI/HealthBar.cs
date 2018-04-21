using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private RectTransform m_HealthContent;
	[SerializeField] private Health m_Health;

	private float m_fraction = 1f;

	private void SetFraction (float fraction)
	{
		m_fraction = fraction;
		UpdateFraction ();
	}

	void Update ()
	{
		SetFraction (m_Health.GetCurrentHealth () / m_Health.GetTotalHealth ());
	}

	protected void UpdateFraction ()
	{
		if (m_fraction > 1)
		{
			m_fraction = 1f;
		}
		else
		if (m_fraction < 0)
		{
			m_fraction = 0;
		}
		m_HealthContent.anchorMax = new Vector2 (m_fraction, 1);
	}
}
