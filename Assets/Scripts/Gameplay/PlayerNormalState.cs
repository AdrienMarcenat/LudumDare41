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
	}

	public override void Enter ()
	{
		EventManager.Register (PlayerEventManager.MoveLeft, m_ActionMoveLeft);
		EventManager.Register (PlayerEventManager.MoveRight, m_ActionMoveRight);
		EventManager.Register (PlayerEventManager.MoveStop, m_ActionMoveStopt);
		EventManager.Register (PlayerEventManager.Fire, m_ActionFire);
	}

	public override void Exit ()
	{
		EventManager.Unregister (PlayerEventManager.MoveLeft, m_ActionMoveLeft);
		EventManager.Unregister (PlayerEventManager.MoveRight, m_ActionMoveRight);
		EventManager.Unregister (PlayerEventManager.MoveStop, m_ActionMoveStopt);
		EventManager.Unregister (PlayerEventManager.Fire, m_ActionFire);
	}

	private void MoveLeft (CommandModifier modifier)
	{
		m_Body.Move (-1, 0);
	}

	private void MoveRight (CommandModifier modifier)
	{
		m_Body.Move (1, 0);
	}

	private void MoveStop (CommandModifier modifier)
	{
		m_Body.Move (0, 0);
	}

	private void Fire (CommandModifier modifier)
	{
		if (modifier.numberModifier > 0)
		{
			for (int i = 0; i < modifier.numberModifier; i++)
			{
				m_WeaponManager.Fire (0);
			}
		}
	}

	private void GameOver ()
	{
		requestStackPop ();
		requestStackPush ((int)PlayerStates.ID.GameOver);
	}

	private void Damage ()
	{
		requestStackPush ((int)PlayerStates.ID.Invincible);
	}
}

