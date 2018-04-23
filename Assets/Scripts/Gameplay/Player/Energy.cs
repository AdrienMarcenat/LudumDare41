using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour
{
	[SerializeField] protected float m_TotalEnergy;
	[SerializeField] protected float m_RefillRate = 1;
	protected float m_CurrentEnergy;

	public delegate void SimpleEvent ();

	public event SimpleEvent EnergyChanged;

	protected void Start ()
	{
		Reset ();
		StartCoroutine (RefillRoutine ());
	}

	public void SetEnergy (float value)
	{
		m_CurrentEnergy = value;
	}

	public void Reset ()
	{
		m_CurrentEnergy = m_TotalEnergy;
	}

	public void LoseEnergy (float energyConsumed)
	{
		m_CurrentEnergy = Mathf.Max (0, m_CurrentEnergy - energyConsumed);

		if (EnergyChanged != null)
			EnergyChanged ();
	}

	IEnumerator RefillRoutine ()
	{
		while (true) {
			yield return new WaitForSeconds (1);
			if (m_CurrentEnergy < m_TotalEnergy) {
				m_CurrentEnergy = Mathf.Clamp (m_CurrentEnergy + m_RefillRate, 0, m_TotalEnergy);
				if (EnergyChanged != null)
					EnergyChanged ();
			}
		}
	}

	public float GetCurrentEnergy ()
	{
		return m_CurrentEnergy;
	}

	public float GetTotalEnergy ()
	{
		return m_TotalEnergy;
	}
}

