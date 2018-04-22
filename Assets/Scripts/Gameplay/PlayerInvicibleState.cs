using UnityEngine;
using System.Collections;

public class PlayerInvicibleState : FSMState
{
	private Health m_Health;
	private SpriteRenderer m_Sprite;
	[SerializeField] float m_InvulnerabilitySeconds;
	[SerializeField] float m_BlinkingRate;
	private float m_InvulnerabilitySecondsDelay;

	protected override void Awake ()
	{
		ID = (int)PlayerStates.ID.Invincible;
		base.Awake ();

		m_Health = GetComponent<Health> ();
		m_Sprite = GetComponentInChildren<SpriteRenderer> ();
	}

	public override void Enter ()
	{
		m_Health.Enable (false);
		StartCoroutine (InvulnerabilityRoutine ());
	}

	public override void Exit ()
	{
		m_Sprite.enabled = true;
		m_Health.Enable (true);
	}

	IEnumerator InvulnerabilityRoutine ()
	{
		m_InvulnerabilitySecondsDelay = m_InvulnerabilitySeconds;
		while (m_InvulnerabilitySecondsDelay > 0)
		{
			m_InvulnerabilitySecondsDelay -= Time.deltaTime + m_BlinkingRate;
			m_Sprite.enabled = !m_Sprite.enabled;
			yield return new WaitForSeconds (m_BlinkingRate);
		}
		requestStackPop ();
	}
}

