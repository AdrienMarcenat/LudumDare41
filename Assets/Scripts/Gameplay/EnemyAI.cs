using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

public class EnemyAI : MonoBehaviour
{
	[SerializeField] private float m_AimedPrecision;
	[SerializeField] private float m_FireSalveNumber;
	[SerializeField] private float m_FireRate;
	[SerializeField] private float m_SizeModifier;
	[SerializeField] private Transform m_Target;

	private WeaponManager m_WeaponManager;
	private float m_FireDelay;

	void Start ()
	{
		m_WeaponManager = GetComponent<WeaponManager> ();
		m_FireDelay = m_FireRate;
	}

	void Update ()
	{
		if (m_FireDelay < m_FireRate)
		{
			m_FireDelay += Time.deltaTime;
		}
		else
		{
			m_WeaponManager.Fire (0, m_FireSalveNumber, m_SizeModifier, m_Target.position - transform.position);
			m_FireDelay = 0;
		}
	}

	public void SetTarget (Transform target)
	{
		m_Target = target;
	}
}

