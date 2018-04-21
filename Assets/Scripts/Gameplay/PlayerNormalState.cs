using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerNormalState : FSMState
{
	private Health health;
	private MovingObject body;
	private WeaponManager m_WeaponManager;

	private UnityAction m_ActionMoveLeft;
	private UnityAction m_ActionMoveRight;
	private UnityAction m_ActionMoveStopt;
	private UnityAction m_ActionFire;

	protected override void Awake ()
	{
		ID = (int)PlayerStates.ID.Normal;
		base.Awake ();
	
		health = GetComponent<Health> ();
		body = GetComponent<MovingObject> ();
		m_WeaponManager = GetComponent<WeaponManager> ();

		m_ActionMoveLeft = new UnityAction (MoveLeft);
		m_ActionMoveRight = new UnityAction (MoveRight);
		m_ActionMoveStopt = new UnityAction (MoveStop);
		m_ActionFire = new UnityAction (Fire);
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

	private void MoveLeft ()
	{
		body.Move (-1, 0);
		Debug.Log ("move left");
	}

	private void MoveRight ()
	{
		body.Move (1, 0);
		Debug.Log ("move right");
	}

	private void MoveStop ()
	{
		body.Move (0, 0);
		Debug.Log ("move stop");
	}

	private void Fire ()
	{
		m_WeaponManager.Fire (0);
		Debug.Log ("Fire");
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

