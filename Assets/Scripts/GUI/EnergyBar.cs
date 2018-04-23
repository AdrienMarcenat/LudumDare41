using UnityEngine;
using System.Collections;

public class EnergyBar : MonoBehaviour
{
	[SerializeField] private RectTransform m_EnergyContent;
	[SerializeField] private Energy m_Energy;

	private float m_fraction = 1f;

	private void SetFraction (float fraction)
	{
		m_fraction = fraction;
		UpdateFraction ();
	}

	void Update ()
	{
		SetFraction (m_Energy.GetCurrentEnergy () / m_Energy.GetTotalEnergy ());
	}

	protected void UpdateFraction ()
	{
		if (m_fraction > 1) {
			m_fraction = 1f;
		} else if (m_fraction < 0) {
			m_fraction = 0;
		}
		m_EnergyContent.anchorMax = new Vector2 (m_fraction, 1);
	}
}

