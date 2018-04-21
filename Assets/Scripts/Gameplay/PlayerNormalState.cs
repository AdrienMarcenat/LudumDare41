using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerNormalState : FSMState
{
	private PlayerEventManager playerEventManager;
	private Health health;
	private MovingObject body;

	private UnityAction m_ActionMoveLeft;
	private UnityAction m_ActionMoveRight;

	protected override void Awake ()
	{
		ID = (int)PlayerStates.ID.Normal;
		base.Awake ();
	
		playerEventManager = GetComponent<PlayerEventManager> ();
		health = GetComponent<Health> ();
		body = GetComponent<MovingObject> ();

		m_ActionMoveLeft = new UnityAction (MoveLeft);
		m_ActionMoveRight = new UnityAction (MoveRight);
	}

	public override void Enter ()
	{
		EventManager.Register (PlayerEventManager.MoveLeft, m_ActionMoveLeft);
		EventManager.Register (PlayerEventManager.MoveRight, m_ActionMoveRight);
	}

	public override void Exit ()
	{
		EventManager.Unregister (PlayerEventManager.MoveLeft, m_ActionMoveLeft);
		EventManager.Unregister (PlayerEventManager.MoveRight, m_ActionMoveRight);
	}

	private void MoveLeft ()
	{
		body.Move (1, 0);
		Debug.Log ("move left");
	}

	private void MoveRight ()
	{
		body.Move (-1, 0);
		Debug.Log ("move right");
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

