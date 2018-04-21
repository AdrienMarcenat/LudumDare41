using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public RectTransform healthContent;

	private float m_fraction;
	public float fraction
	{ 
		get
		{
			return m_fraction;
		}
		set
		{
			m_fraction = value;
			UpdateFraction ();
		}
	}

	protected void UpdateFraction ()
	{
		if(m_fraction>1)
		{
			m_fraction = 1f;
		}
		else if (m_fraction<0)
		{
			m_fraction = 0;
		}
		healthContent.anchorMax = new Vector2 (m_fraction, 1);
	}
}
