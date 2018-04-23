using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
	private MovingObject body;

	void Start ()
	{
		body = GetComponent<MovingObject> ();
	}

	void Update ()
	{
		MoveEnemy ();
	}

	private void MoveEnemy ()
	{
		float horizontal = m_Target.transform.position.x - transform.position.x;
		float vertical = m_Target.transform.position.y - transform.position.y;

		body.Move (horizontal, vertical);
	}
}
