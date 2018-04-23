using UnityEngine;
using System.Collections;

public class PlayerInvicibleState : FSMState
{
	private Health m_Health;
	[SerializeField] private SpriteRenderer m_Sprite;
	[SerializeField] private float m_InvulnerabilitySeconds;
	[SerializeField] private float m_BlinkingRate;
	private float m_InvulnerabilitySecondsDelay;

	public static bool IsInvicible = false;

	protected override void Awake ()
	{
		ID = (int)PlayerStates.ID.Invincible;
		base.Awake ();

		m_Health = GetComponent<Health> ();
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
		yield return null;

		if (!IsInvicible) {
			IsInvicible = true;
			if (m_Health.GetCurrentHealth () / m_Health.GetTotalHealth () < 0.2f) {
				EventManager.TriggerEvent ("LowHealth", new CommandModifier (1, 1, 1));	
			} else {
				EventManager.TriggerEvent ("Hit", new CommandModifier (1, 1, 1));
			}

			m_InvulnerabilitySecondsDelay = m_InvulnerabilitySeconds;
			while (m_InvulnerabilitySecondsDelay > 0) {
				m_InvulnerabilitySecondsDelay -= Time.deltaTime + m_BlinkingRate;
				m_Sprite.enabled = !m_Sprite.enabled;
				yield return new WaitForSeconds (m_BlinkingRate);
			}
			IsInvicible = false;
		}
		requestStackPop ();
	}
}

