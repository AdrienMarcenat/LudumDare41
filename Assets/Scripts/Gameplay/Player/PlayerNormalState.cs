using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerNormalState : FSMState
{
	private Health m_Health;
	private MovingObject m_Body;
	private WeaponManager m_WeaponManager;

	private UnityAction<CommandModifier> m_ActionMoveLeft;
	private UnityAction<CommandModifier> m_ActionMoveRight;
	private UnityAction<CommandModifier> m_ActionMoveStopt;
	private UnityAction<CommandModifier> m_ActionFire;
	private UnityAction<CommandModifier> m_ActionLaser;
	private UnityAction<CommandModifier> m_ActionShield;
	private UnityAction<CommandModifier> m_ActionGodState;
	private UnityAction<CommandModifier> m_ActionAutodestruction;

	protected override void Awake ()
	{
		ID = (int)PlayerStates.ID.Normal;
		base.Awake ();
	
		m_Health = GetComponent<Health> ();
		m_Body = GetComponent<MovingObject> ();
		m_WeaponManager = GetComponent<WeaponManager> ();

		m_ActionMoveLeft = new UnityAction<CommandModifier> (MoveLeft);
		m_ActionMoveRight = new UnityAction<CommandModifier> (MoveRight);
		m_ActionMoveStopt = new UnityAction<CommandModifier> (MoveStop);
		m_ActionFire = new UnityAction<CommandModifier> (Fire);
		m_ActionLaser = new UnityAction<CommandModifier> (Laser);
		m_ActionShield = new UnityAction<CommandModifier> (Shield);
		m_ActionGodState = new UnityAction<CommandModifier> (GodState);
		m_ActionAutodestruction = new UnityAction<CommandModifier> (Autodestruction);
	}

	public override void Enter ()
	{
		EventManager.Register (PlayerEventManager.MoveLeft, m_ActionMoveLeft);
		EventManager.Register (PlayerEventManager.MoveRight, m_ActionMoveRight);
		EventManager.Register (PlayerEventManager.MoveStop, m_ActionMoveStopt);
		EventManager.Register (PlayerEventManager.Fire, m_ActionFire);
		EventManager.Register (PlayerEventManager.Laser, m_ActionLaser);
		EventManager.Register (PlayerEventManager.Shield, m_ActionShield);
		EventManager.Register (PlayerEventManager.Pause, m_ActionGodState);
		EventManager.Register (PlayerEventManager.Autodestruction, m_ActionAutodestruction);

		m_Health.SimpleDamage += Damage;
		m_Health.GameOver += GameOver;
	}

	public override void Exit ()
	{
		EventManager.Unregister (PlayerEventManager.MoveLeft, m_ActionMoveLeft);
		EventManager.Unregister (PlayerEventManager.MoveRight, m_ActionMoveRight);
		EventManager.Unregister (PlayerEventManager.MoveStop, m_ActionMoveStopt);
		EventManager.Unregister (PlayerEventManager.Fire, m_ActionFire);
		EventManager.Unregister (PlayerEventManager.Laser, m_ActionLaser);
		EventManager.Unregister (PlayerEventManager.Shield, m_ActionShield);
		EventManager.Unregister (PlayerEventManager.Pause, m_ActionGodState);
		EventManager.Unregister (PlayerEventManager.Autodestruction, m_ActionAutodestruction);

		m_Health.SimpleDamage -= Damage;
		m_Health.GameOver -= GameOver;
	}

	private void MoveLeft (CommandModifier modifier)
	{
		m_Body.Move (-1 * modifier.speedModifier, 0);
	}

	private void MoveRight (CommandModifier modifier)
	{
		m_Body.Move (1 * modifier.speedModifier, 0);
	}

	private void MoveStop (CommandModifier modifier)
	{
		m_Body.Move (0, 0);
	}

	private void Fire (CommandModifier modifier)
	{
		m_WeaponManager.Fire (0, modifier.numberModifier, modifier.sizeModifier, Vector3.up);
	}

	private void Laser (CommandModifier modifier)
	{
		Energy energy = GetComponent<Energy> ();
		if (energy != null) {
			if (energy.GetCurrentEnergy () > 0) {
				m_WeaponManager.Fire (1, 1, modifier.sizeModifier, Vector3.up);
				energy.LoseEnergy (30 * modifier.sizeModifier);
			}
		}
	}

	private void Autodestruction (CommandModifier cm)
	{
		m_Health.LoseHealth (1000);
	}

	private void GameOver ()
	{
		requestStackPop ();
		requestStackPush ((int)PlayerStates.ID.GameOver);
		requestStackPush ((int)PlayerStates.ID.God);
	}

	private void Damage ()
	{
		if (!PlayerInvicibleState.IsInvicible) {
			requestStackPush ((int)PlayerStates.ID.Invincible);
		}
	}

	private void GodState (CommandModifier cm)
	{
		requestStackPop ();
		requestStackPush ((int)PlayerStates.ID.God);
	}

	private void Shield (CommandModifier cm)
	{
		requestStackPush ((int)PlayerStates.ID.Shield);
	}
}

