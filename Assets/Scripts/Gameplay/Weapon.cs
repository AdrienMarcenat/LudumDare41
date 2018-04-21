using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCommand
{
	public float number;
	public float sizeModifier;
	public Vector3 target;

	public FireCommand (float number, float sizeModifier, Vector3 target)
	{
		this.number = number;
		this.sizeModifier = sizeModifier;
		this.target = target;
	}

	public void Decrease ()
	{
		number--;
	}
}

public class Weapon : MonoBehaviour
{
	[SerializeField] int type;
	[SerializeField] int totalAmmo = int.MaxValue;
	[SerializeField] int currentAmmo;
	[SerializeField] float ammoVelocity;
	[SerializeField] float m_FireRate;

	[SerializeField] GameObject bulletPrefab;
	[SerializeField] AudioClip fireSound;

	private List<FireCommand> m_FireCommands;
	private float m_FireDelay;
	private float m_FireCommandNumber;

	void Start ()
	{
		m_FireCommands = new List<FireCommand> ();
		currentAmmo = totalAmmo;
		m_FireDelay = m_FireRate;
	}

	void Update ()
	{
		if (m_FireDelay < m_FireRate)
		{
			m_FireDelay += Time.deltaTime;
		}
		else
		if (m_FireCommands.Count > 0)
		{
			FireInternal ();
		}
	}

	public void SetAmmo (int amount)
	{
		currentAmmo += amount;
		currentAmmo = Mathf.Clamp (currentAmmo, 0, totalAmmo);
	}

	public void Fire (float numberModifier, float sizeModifier, Vector3 target)
	{
		m_FireCommands.Add (new FireCommand (numberModifier, sizeModifier, target));
	}

	private void FireInternal ()
	{
		if (currentAmmo == 0)
		{
			return;
		}

		m_FireDelay = 0;
		SetAmmo (-1);
		SoundManager.PlayMultiple (fireSound);

		FireCommand command = m_FireCommands [0];
		Vector3 fireDirection = command.target.normalized;

		GameObject bullet = Instantiate (bulletPrefab);
		bullet.transform.position = transform.position;
		bullet.transform.localScale *= command.sizeModifier;
		bullet.GetComponent<Rigidbody2D> ().velocity = ammoVelocity * fireDirection;

		command.Decrease ();
		if (command.number == 0)
		{
			m_FireCommands.RemoveAt (0);
		}
	}

	public void Reload ()
	{
		currentAmmo = totalAmmo;
	}

	public int GetWeaponType ()
	{
		return type;
	}

	public int GetAmmo ()
	{
		return currentAmmo;
	}
}
