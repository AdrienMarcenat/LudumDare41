using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	[SerializeField] protected float m_TotalHealth;
	protected float m_CurrentHealth;
	private bool m_Enable;
	private float m_DamageModifier;

	public delegate void SimpleEvent ();

	public event SimpleEvent GameOver;
	public event SimpleEvent SimpleDamage;

	public delegate void DamageAction (float damage);

	public event DamageAction Damage;

	protected void Start ()
	{
		m_CurrentHealth = m_TotalHealth;
		m_Enable = true;
		m_DamageModifier = 1.0f;
	}

	public void LoseHealth (float damage)
	{
		if (!m_Enable)
			return;
		
		if (Damage != null)
			Damage (damage);

		m_CurrentHealth = Mathf.Max (0, m_CurrentHealth - m_DamageModifier * damage);

		if (SimpleDamage != null)
			SimpleDamage ();
		
		CheckIfGameOver ();
	}

	public float GetCurrentHealth ()
	{
		return m_CurrentHealth;
	}

	public float GetTotalHealth ()
	{
		return m_TotalHealth;
	}

	public void Heal ()
	{
		m_CurrentHealth = m_TotalHealth;
	}

	private void CheckIfGameOver ()
	{
		if (m_CurrentHealth <= 0 && GameOver != null)
			GameOver ();
	}

	public void Enable (bool enable)
	{
		this.m_Enable = enable;
	}

	public void SetDamageModifier (float modifier)
	{
		m_DamageModifier = modifier;
	}
}

