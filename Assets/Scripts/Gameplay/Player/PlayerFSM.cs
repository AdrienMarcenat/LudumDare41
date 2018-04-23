﻿using UnityEngine;
using System.Collections;

namespace PlayerStates
{
	public enum ID
	{
		Normal = 0,
		Invincible = 1,
		GameOver = 2,
		God = 3
	}
}

public class PlayerFSM : FSM
{
	[SerializeField] Transform m_SpawningPosition;

	protected override void Awake ()
	{
		base.Awake ();
		Reset ();
	}

	// Reset all components
	public void Reset ()
	{
		ClearStates ();
		PushState ((int)PlayerStates.ID.Normal);
		GetComponent<Health> ().Reset ();
		GetComponent<MovingObject> ().Reset ();
		GetComponent<WeaponManager> ().Reset ();
		GetComponent<LetterInventory> ().Reset ();
		if (m_SpawningPosition == null)
			m_SpawningPosition = GameObject.Find ("PlayerSpawningPosition").transform;
		if (m_SpawningPosition != null)
			transform.position = m_SpawningPosition.position;
	}
}
