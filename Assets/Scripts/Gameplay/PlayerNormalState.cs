using UnityEngine;
using System.Collections;

public class PlayerNormalState : FSMState
{
	private PlayerEventManager playerEventManager;
	private Health health;
	private MovingObject body;

	protected override void Awake ()
	{
		ID = (int)PlayerStates.ID.Normal;
		base.Awake ();
	
		playerEventManager = GetComponent<PlayerEventManager> ();
		health = GetComponent<Health> ();
		body = GetComponent<MovingObject> ();
	}

	public override void Enter ()
	{
		playerEventManager.Move += MovePlayer;
		playerEventManager.HealthPack += health.Heal;
		health.SimpleDamage += Damage;
		health.GameOver += GameOver;
	}

	public override void Exit ()
	{
		playerEventManager.Move -= MovePlayer;
		playerEventManager.HealthPack -= health.Heal;
		health.SimpleDamage -= Damage;
		health.GameOver -= GameOver;
	}

	private void MovePlayer (float x, float y)
	{
		body.Move (x, y);

		Vector3 direction = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position).normalized;
		Quaternion rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + 90);
		transform.rotation = rotation;
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

