using UnityEngine;
using System.Collections;

public class EnemyGUI : MonoBehaviour
{
	private Health m_Health;

	[SerializeField] SpriteRenderer m_HealthBar;

	void Awake ()
	{
		m_Health = GetComponentInParent<Health> ();
	}

	void OnEnable ()
	{
		m_Health.SimpleDamage += UpdateUI;
	}

	void OnDisable ()
	{
		m_Health.SimpleDamage -= UpdateUI;
	}

	private void HealthBarEnable ()
	{
		m_HealthBar.enabled = true;
	}

	private void UpdateUI ()
	{
		float currentHealth = m_Health.GetCurrentHealth ();
		float totalHealth = m_Health.GetTotalHealth ();

		Vector3 scale = m_HealthBar.transform.localScale;
		m_HealthBar.transform.localScale = new Vector3 (currentHealth / totalHealth, scale.y, scale.z);
	}
}