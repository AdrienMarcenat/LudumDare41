using UnityEngine;
using System.Collections;

public class PlayerShieldState : FSMState
{
	private Energy m_Energy;
	private Health m_Health;
	[SerializeField] private SpriteRenderer m_Sprite;
	[SerializeField] private float m_EnergyDrainedPerSecond;

	public static bool IsShieldActive = false;

	protected override void Awake ()
	{
		ID = (int)PlayerStates.ID.Shield;
		base.Awake ();

		m_Sprite.enabled = false;
		m_Energy = GetComponent<Energy> ();
		m_Health = GetComponent<Health> ();
	}

	public override void Enter ()
	{
		m_Sprite.enabled = true;
		m_Health.Enable (false);
		StartCoroutine (ShieldRoutine ());
	}

	public override void Exit ()
	{
		StopAllCoroutines ();
		m_Sprite.enabled = false;
		m_Health.Enable (true);
	}

	IEnumerator ShieldRoutine ()
	{
		yield return null;

		if (!IsShieldActive) {
			IsShieldActive = true;
			while (IsShieldActive) {
				m_Energy.LoseEnergy (m_EnergyDrainedPerSecond);
				if (m_Energy.GetCurrentEnergy () == 0)
					break;
				yield return new WaitForSeconds (1);
			}
		}
		IsShieldActive = false;
		requestStackPop ();
	}
}

